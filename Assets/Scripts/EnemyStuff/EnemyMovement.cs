using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float giveUpDistance;
    [SerializeField] private float chaseCheckAngle;
    private Transform currentTarget;
    
    private EnemyStates currentState;
    private bool waiting;

    private Vector3 velocity;
    
    [Header("Ground Check")] 
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;

    public EnemyStates CurrentState()
    {
        return currentState;
    }
    
    void Start()
    {
        if(playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        if(agent == null)
            Destroy(gameObject);
        
        agent.stoppingDistance = 0.2f;
        
        CheckGrounded();
        if (!isGrounded)
            Destroy(gameObject);
        
        transform.forward = (playerTransform.position - transform.position).normalized;
    }
    
    void FixedUpdate()
    {
        if (currentState == EnemyStates.Idle)
        {
            animator.SetBool("isIdle", true);
            if(!waiting)
                StartCoroutine(GoToTarget(1));

            if (InRange() && InFOV())
            {
                currentState = EnemyStates.Chase;
            }
        } 
        else if (currentState == EnemyStates.Chase)
        {
            animator.SetBool("isWalking", true);
            agent.SetDestination(playerTransform.position);
            if (outOfRange())
            {
                CheckTargetDistance();
                animator.SetBool("isWalking", false);
            }
        }
    }

    private void CheckTargetDistance()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentState = EnemyStates.Idle;
            animator.SetBool("isWalking", false);
        }
    }

    private IEnumerator GoToTarget(float waitTime)
    {
        waiting = true;
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
    
    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
