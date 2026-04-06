using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private int health;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float speedModifier;
    
    [Space(5)]
    [Header("Aim Movement")]
    [SerializeField] private float moveSpeedAim = 2;
    [SerializeField] private float rotationSpeedAim = 10;
    [SerializeField] private Transform aimTrack;
    [SerializeField] private float maxAimHeight;
    [SerializeField] private float minAimHeight;

    [Space(10)]
    [Header("Ground Check")]
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    public event Action OnJumpEvent;
    public event Action<PlayerState> OnStateUpdated;
    
    private Vector2 _moveInput;
    private Vector2 lookInput;
    private Vector3 _camForward;
    private Vector3 _camRight;
    private Vector3 _moveDirection;
    private CharacterController _characterController;
    private Quaternion _targetRotation;
    private Vector3 _velocity;
    private bool _isGrounded;
    private PlayerState currentState;
    private Vector3 defaultAimTrackerPos;
    private Vector3 tempAimTrackerPos;


    [Space(10)]
    [SerializeField] private GameManager gm;

    public bool IsGrounded()
    {
        return _isGrounded;
    }

    public Vector3 GetPlayerVelocity()
    {
        return _velocity;
    }
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
        if(gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        currentState = PlayerState.Explore;
        OnStateUpdated?.Invoke(currentState);

        defaultAimTrackerPos = aimTrack.localPosition;
    }
    
    void Update()
    {
        if (currentState == PlayerState.Explore)
        {
            CalcExplorationMovement();
            aimTrack.localPosition = defaultAimTrackerPos;
        }
        else if (currentState == PlayerState.Aim)
        {
            CalcAimMovement();
            UpdateAimTracker();
        }
        
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        if(_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -0.2f;
        }
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if(_isGrounded)
        {
            _velocity.y = jumpVelocity;
            OnJumpEvent?.Invoke();
        }
    }

    public void OnAim(InputValue value)
    {
        currentState = value.isPressed ? PlayerState.Aim : PlayerState.Explore;
        
        OnStateUpdated?.Invoke(currentState);

        if (currentState == PlayerState.Aim)
        {
            _camForward = playerCamera.transform.forward;
            _camForward.y = 0;
            _camForward.Normalize();
            transform.rotation = Quaternion.LookRotation(_camForward);
        }
    }
    
    private void CalcExplorationMovement()
    {
        _camForward = playerCamera.transform.forward;
        _camRight = playerCamera.transform.right;
        _camForward.y = 0;
        _camRight.y = 0;
        _camForward.Normalize();
        _camRight.Normalize();

        _moveDirection = _camRight * _moveInput.x + _camForward * _moveInput.y;

        if(_moveDirection.sqrMagnitude > 0.01f)
        {
            _targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        //Calculate gravity
        _velocity = _velocity.y * Vector3.up + (moveSpeed * speedModifier) * _moveDirection;
        _velocity.y += gravity * Time.deltaTime;
    }

    private void CalcAimMovement()
    {
        //Rotate around Y-Axis, based on horizontal input
        transform.Rotate(Vector3.up, rotationSpeedAim * lookInput.x * Time.deltaTime);

        _moveDirection = _moveInput.x * transform.right + _moveInput.y * transform.forward;
        
        _velocity = _velocity.y * Vector3.up + (moveSpeedAim * speedModifier) * _moveDirection;
        _velocity.y += gravity * Time.deltaTime;
    }

    private void UpdateAimTracker()
    {
        tempAimTrackerPos = aimTrack.localPosition;
        
        tempAimTrackerPos.y += lookInput.y * rotationSpeedAim * Time.deltaTime;
        
        tempAimTrackerPos.y = Mathf.Clamp(tempAimTrackerPos.y, minAimHeight, maxAimHeight);
        
        aimTrack.localPosition = tempAimTrackerPos;
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics.SphereCast(
            transform.position + groundCheckOffset,
            groundCheckRadius,
            Vector3.down,
            out RaycastHit hit,
            groundCheckDistance,
            groundLayer
        );
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        gm.DisplayHealth(health);

        if (health <= 0)
        {
            gm.DeadPlayer();
        }
    }

    public void Heal(int ammount)
    {
        health += ammount;
        gm.DisplayHealth(health);
    }

    public void IncreaseSpeed(float ammount)
    {
        speedModifier += ammount;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + groundCheckOffset, groundCheckRadius);
        Gizmos.DrawSphere(transform.position + groundCheckOffset + Vector3.down * groundCheckDistance, groundCheckRadius);
        Gizmos.DrawCube(transform.position + groundCheckOffset + Vector3.down * groundCheckDistance/2, 
                    new Vector3(1.5f* groundCheckRadius, groundCheckDistance , 1.5f * groundCheckRadius) );
    }
}