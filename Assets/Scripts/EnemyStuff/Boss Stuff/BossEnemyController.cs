using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyController : MonoBehaviour
{
    [SerializeField] private int damage;

    private GameObject player;

    private BossState currentState;
    [SerializeField] private GameManager gm;
    [SerializeField] private NavMeshAgent agent;
    
    [SerializeField] private Transform fistPos;
    [SerializeField] private float fistSpeed;
    private Vector3 fistStartPos;
    private float attackTimer;

    private bool seenPlayer;
    
    private float distance;

    private bool isHunting;

    public int Damage()
    {
        return damage;
    }

    public GameObject Player()
    {
        return player;
    }

    public bool IsHunting()
    {
        return isHunting;
    }

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        
        if(gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        currentState = BossState.Idle;
        
        fistStartPos = fistPos.localPosition;

        agent.stoppingDistance = 5;
        agent.enabled = false;
    }
    
    void Update()
    {
        if (seenPlayer && currentState == BossState.Idle)
        {
            currentState = BossState.Hunting;
            agent.enabled = true;
        }

        if (currentState == BossState.Idle)
        {
            isHunting = false;
            agent.enabled = false;
        }
        
        if (currentState == BossState.Hunting)
        {
            isHunting = true;
            FindPlayerDistance();
            agent.SetDestination(player.transform.position);
        }

        if (currentState == BossState.Attacking)
        {
            isHunting = false;

            StartCoroutine(AttackTime(1.5f));
            
            SmashFists();
        }
    }

    private void FindPlayerDistance()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            currentState = BossState.Attacking;
        }
    }

    private void SmashFists()
    {
        fistPos.Translate(Vector3.down * fistSpeed * Time.deltaTime);
    }

    private IEnumerator AttackTime(float timer)
    {
        yield return new WaitForSeconds(timer);
        ResetFists();
    }

    public void ResetFists()
    {
        currentState = seenPlayer ? BossState.Hunting : BossState.Idle;
        fistPos.localPosition = fistStartPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seenPlayer = true;
        }
    }

    private void OnDestroy()
    {
        gm.WonGame();
    }
}
