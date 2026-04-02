using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float giveUpDistance;
    [SerializeField] private float chaseCheckAngle;
    private Transform currentTarget;
    
    private EnemyStates currentState;
    private bool waiting;
    
    void Start()
    {
        PickRandomPoint();
        agent.stoppingDistance = 0.2f;
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
        else if (currentState == EnemyStates.Patrol)
        {
            animator.SetBool("isWalking", true);
            CheckTargetDistance();
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
                currentState = EnemyStates.Idle;
                animator.SetBool("isWalking", false);
            }
        }
    }

    private void PickRandomPoint()
    {
        if (patrolPoints.Length <= 0) return;
        
        currentTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];
        
        agent.SetDestination(currentTarget.position);
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
        Debug.Log("hi");
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        currentState = EnemyStates.Patrol;
        animator.SetBool("isIdle", false);
        PickRandomPoint();
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
}
