using System;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemyController : MonoBehaviour
{
    [SerializeField] private int damage;

    private GameObject player;

    private BossState currentState;
    [SerializeField] private EnemyController ec;
    [SerializeField] private GameManager gm;
    
    [SerializeField] private Transform fistPos;
    [SerializeField] private float fistSpeed;
    private Vector3 fistStartPos;
    private float attackTimer;

    private bool seenPlayer;
    
    private float distance;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (ec == null)
            gameObject.GetComponent<EnemyController>();
        
        if(gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        ec.enabled = false;
        
        currentState = BossState.Idle;
        
        fistStartPos = fistPos.localPosition;
    }
    
    void Update()
    {
        if (seenPlayer && currentState == BossState.Idle)
        {
            currentState = BossState.Hunting;
        }
        
        if (currentState == BossState.Hunting)
        {
            ec.enabled = true;
            FindPlayerDistance();
        }

        if (currentState == BossState.Attacking)
        {
            ec.enabled = false;
            
            if (attackTimer >= 2)
            {
                currentState = seenPlayer ? BossState.Hunting : BossState.Idle;
                fistPos.localPosition = fistStartPos;
                attackTimer = 0;
                return;
            }
            
            SmashFists();

            attackTimer += Time.deltaTime;
        }
    }

    private void FindPlayerDistance()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(distance);

        if (distance < 5)
        {
            currentState = BossState.Attacking;
        }
    }

    private void SmashFists()
    {
        Debug.Log(fistPos.position.y);
        
        fistPos.Translate(Vector3.down * fistSpeed * Time.deltaTime);
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
