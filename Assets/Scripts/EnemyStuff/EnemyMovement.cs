using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float giveUpDistance;
    [SerializeField] private float chaseCheckAngle;
    private Transform currentTarget;
    
    private EnemyStates currentState;
    private bool waiting;

    private Vector3 velocity;
    
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
    }
    
    void FixedUpdate()
    {
        //The Idle state is only active when the enemy is falling, otherwise it's doing something
        if (currentState == EnemyStates.Grounded)
        {
            if (!waiting)
            {
                StartCoroutine(LookForGround(1));
            }
            
            if (InRange() && InFOV())
            {
                currentState = EnemyStates.Chase;
                animator.SetBool("isIdle", false);
            }
        }
        else if (currentState == EnemyStates.Chase)
        {
            animator.SetBool("isWalking", true);
            agent.SetDestination(playerTransform.position);
            if (outOfRange())
            {
                CheckTargetDistance();
            }
        }
    }

    private void CheckTargetDistance()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentState = EnemyStates.Grounded;
            animator.SetBool("isWalking", false);
        }
    }

    private IEnumerator LookForGround(float waitTime)
    {
        waiting = true;
        animator.SetBool("isIdle", true);
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("isIdle", false);
        waiting = false;
    }

    private bool InRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance;
    }

    private bool outOfRange()
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
        if (other.gameObject.layer == 6)
        {
            animator.SetBool("isIdle", true);
            currentState = EnemyStates.Grounded;
            
            gameObject.AddComponent(typeof(NavMeshAgent));
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 0.2f;
            agent.baseOffset = groundOffset;
            rb.isKinematic = false;
            
            transform.forward = (playerTransform.position - transform.position).normalized;
        }
    }
}
