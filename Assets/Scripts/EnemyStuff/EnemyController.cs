using System;
using UnityEngine;
using UnityEngine.XR;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    private Vector3 velocity;
    
    [SerializeField] private Transform playerPos;

    [Header("Ground Check")] 
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    
    private bool isGrounded;

    public bool IsGrounded()
    {
        return isGrounded;
    }
    
    void Start()
    {
        if (playerPos == null)
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        FindPlayer();
        HandleMovement();
        controller.Move(velocity * Time.deltaTime);
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
        transform.LookAt(playerPos);
    }

    private void HandleMovement()
    {
        velocity = Vector3.up * velocity.y + transform.forward * speed;
        velocity.y += gravity * Time.deltaTime;
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
    
    void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + groundCheckOffset, groundCheckRadius);
        Gizmos.DrawSphere(transform.position + groundCheckOffset + Vector3.down * groundCheckDistance, groundCheckRadius);
        Gizmos.DrawCube(transform.position + groundCheckOffset + Vector3.down * groundCheckDistance/2, 
            new Vector3(1.5f* groundCheckRadius, groundCheckDistance , 1.5f * groundCheckRadius) );
    }
}
