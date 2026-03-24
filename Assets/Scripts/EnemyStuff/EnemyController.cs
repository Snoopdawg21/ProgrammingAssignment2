using System;
using System.Transactions;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    private Vector3 velocity;

    [SerializeField] private GameManager gm;
    [SerializeField] private EnemyManager em;
    
    private GameObject player;

    private float lifetime;

    [Header("Ground Check")] 
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    
    private bool isGrounded;
    
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        
        health = maxHealth;
    }
    
    void Update()
    {
        if (lifetime > 10)
        {
            KillEnemy();
        }
        FindPlayer();
        HandleMovement();
        controller.Move(velocity * Time.deltaTime);
        
        if (health == maxHealth)
        {
            lifetime += Time.deltaTime;
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
        transform.LookAt(player.transform);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
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
        
        if(gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        gm.IncreaseScore(maxHealth - health);

        if (health <= 0)
            KillEnemy();
    }

    public void KillEnemy()
    {
        if (em == null)
            em = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemyManager>();

        em.enemyCount--;
        
        Destroy(gameObject);
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
