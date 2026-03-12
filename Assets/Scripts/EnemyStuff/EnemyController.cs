using System;
using System.Transactions;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private int health;
    private int maxHealth;
    public int damage;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    private Vector3 velocity;

    [SerializeField] private GameManager gm;
    
    [SerializeField] private Transform playerPos;
    private GameObject player;

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
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        
        if(gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        playerPos = player.transform;

        maxHealth = Random.Range(1, 2);
        health = maxHealth;
        damage = 1;
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        gm.IncreaseScore(maxHealth - health);

        if (health <= 0)
            KillEnemy();
    }

    void KillEnemy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ow");
            player.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            KillEnemy();
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
