using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private BasicEnemyAttacker bea;
    [SerializeField] private EnemyController ec;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float giveUpDistance;
    [SerializeField] private float chaseCheckAngle;
    [SerializeField] private float attackDistance;
    private Transform currentTarget;
    
    private EnemyStates currentState;
    private bool waiting;
    private bool attacking;
    private bool checkCollisions;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundOffset;

    public EnemyStates CurrentState()
    {
        return currentState;
    }
    
    void Start()
    {
        if(playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        checkCollisions = true;
    }
    
    void FixedUpdate()
    {
        //The Idle state is only active when the enemy is falling, otherwise it's doing something
        if (currentState == EnemyStates.Grounded)
        {
            if (!waiting)
            {
                animator.SetBool("isIdle", true);
                StartCoroutine(WaitTime(1));
            }
            
            if (InRange() && InFOV())
            {
                currentState = EnemyStates.Chase;
                animator.SetBool("isIdle", false);
                ec.DespawnToggle();
            }
        }
        else if (currentState == EnemyStates.Chase)
        {
            animator.SetBool("isWalking", true);
            agent.SetDestination(playerTransform.position);
            if (OutOfRange() || InRange() && InFOV())
            {
                CheckTargetDistance();
            }
        }
        else if (currentState == EnemyStates.Attack)
        {
            bea.AttackPlayer();
            Debug.Log("I'm Attacking");
            checkCollisions = true;
            currentState = EnemyStates.Idle;
        }
    }

    public void CheckTargetDistance()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentState = EnemyStates.Grounded;
            animator.SetBool("isWalking", false);
            ec.DespawnToggle();
        }

        if (Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
        {
            currentState = EnemyStates.Attack;
            animator.SetBool("isWalking", false);
            rb.isKinematic = false;
            agent.enabled = false;
        }
    }

    private IEnumerator WaitTime(float waitTime)
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
    }

    private bool InRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance;
    }

    private bool OutOfRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) >= giveUpDistance;
    }

    private Vector3 directionToPlayer;
    private bool InFOV()
    {
        directionToPlayer = (playerTransform.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, directionToPlayer) <= chaseCheckAngle;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 6 && checkCollisions)
        {
            animator.SetBool("isIdle", true);
            currentState = EnemyStates.Grounded;

            if (agent == null)
            {
                gameObject.AddComponent(typeof(NavMeshAgent));
                agent = gameObject.GetComponent<NavMeshAgent>();
                agent.stoppingDistance = 0.2f;
                agent.baseOffset = groundOffset;
            }
            else
            {
                agent.enabled = true;
            }
            
            ec.DespawnToggle();
            rb.isKinematic = true;
            checkCollisions = false;
            
            transform.forward = (playerTransform.position - transform.position).normalized;
        }
    }
}
