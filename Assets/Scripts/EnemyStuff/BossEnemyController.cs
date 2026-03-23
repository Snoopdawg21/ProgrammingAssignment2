using Unity.VisualScripting;
using UnityEngine;

public class BossEnemyController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    private Vector3 velocity;

    private GameObject player;

    private BossState currentState;
    
    [SerializeField] private Transform fistPos;
    private Vector3 fistStartPos;
    [SerializeField] private float fistSpeed;
    
    [Header("Ground Check")] 
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    
    private bool isGrounded;

    private float a;
    private float b;
    private float c;
    private float distance;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        
        controller = GetComponent<CharacterController>();
        
        currentState = BossState.Idle;
    }
    
    void Update()
    {
        HandleMovement();
        if (currentState == BossState.Hunting)
        {
            FindPlayer();
            controller.Move(velocity * Time.deltaTime);
        }

        if (currentState == BossState.Attacking)
        {
            SmashFists();
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -0.2f;
        }
    }

    private void FindPlayer()
    {
        a = transform.position.x - player.transform.position.x;
        b = transform.position.z - player.transform.position.z;
        c = (a * a) + (b * b);
            
        distance = Mathf.Sqrt(c);
        
        transform.LookAt(player.transform);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        if (distance < 2)
        {
            currentState = BossState.Attacking;
            fistStartPos = fistPos.position;
        }
    }

    private void HandleMovement()
    {
        velocity = Vector3.up * velocity.y + transform.forward * speed;
        velocity.y += gravity * Time.deltaTime;
    }

    private void SmashFists()
    {
        if (fistPos.position.y < -2)
        {
            currentState = BossState.Idle;
            fistPos.position = fistStartPos;
            return;
        }
        Debug.Log(fistPos.position.y);
        
        fistPos.Translate(Vector3.down * fistSpeed * Time.deltaTime);
    }
    
    private void CheckGrounded()
    {
        isGrounded = Physics.SphereCast(
            transform.position + groundCheckOffset,
            groundCheckRadius,
            Vector3.down,
            out RaycastHit hit,
            groundCheckDistance,
            groundLayer
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = BossState.Hunting;
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + groundCheckOffset, groundCheckRadius);
        Gizmos.DrawSphere(transform.position + groundCheckOffset + Vector3.down * groundCheckDistance, groundCheckRadius);
        Gizmos.DrawCube(transform.position + groundCheckOffset + Vector3.down * groundCheckDistance/2, 
            new Vector3(1.5f* groundCheckRadius, groundCheckDistance , 1.5f * groundCheckRadius) );
    }
}
