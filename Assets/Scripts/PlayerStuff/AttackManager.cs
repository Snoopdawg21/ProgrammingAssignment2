using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class AttackManager : MonoBehaviour
{
    private Transform enemyPos;
    private GameObject[] enemies;
    private float enemyDistance;
    [SerializeField] private Quaternion rotationOffset;

    private GameObject newAttackObj;
    private GameObject selectedObj;

    [Header("Attacks")] 
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject fireball;
    public GameObject spinningSword;
    [SerializeField] private Transform swordInstance;
    [SerializeField] private GameObject smokeball;

    private IWeapons weaponInterface;
    
    [Space(10)]
    [Header("Basic Attack Stats")]
    [SerializeField] private float attackTimer;
    [SerializeField] private bool canFire;
    public float fireSpeed = 1;
    public int basicAttackType = 1;

    [Space(10)] [Header("Spinning Sword Stats")] 
    [SerializeField] private float spinSpeed;
    
    [Space(10)] [Header("Smokeball Stats")]
    [SerializeField] private Transform spawnPoint;

    private float a;
    private float b;
    private float c;
    private float tempDistance;

    void Start()
    {
        StartCoroutine(BasicAttackTimer(attackTimer));
    }

    void Update()
    {
        if (canFire)
        {
            StartCoroutine(BasicAttackTimer(attackTimer));
            FindClosestEnemy();
            //Shoot(basicAttackType);
            ThrowBomb();
        }
    }

    private IEnumerator BasicAttackTimer(float timer)
    {
        canFire = false;
        yield return new WaitForSeconds(timer);
        canFire = true;
    }

    public void IncreaseSpinSpeed(float amount)
    {
        spinSpeed += amount;
        spinningSword.GetComponent<SpinningSword>().GetSpeed(spinSpeed);
    }

    private void FindClosestEnemy()
    {
        enemyDistance = 0;
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if(enemies == null) return;

        if (enemies.Length == 1)
        {
            if(enemies[0].GetComponent<EnemyController>().enabled)
                enemyPos = enemies[0].transform;
            
            return;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null) return;
            
            if (!enemies[i].GetComponent<NavMeshAgent>())
            {
                if (i + 1 == enemies.Length) return;

                i++;
            }
            
            //pythagoris
            a = transform.position.x - enemies[i].transform.position.x;
            b = transform.position.z - enemies[i].transform.position.z;
            c = (a * a) + (b * b);
            
            tempDistance = Mathf.Sqrt(c);
            if (tempDistance < enemyDistance || enemyDistance == 0)
            {
                enemyDistance = tempDistance;
                enemyPos = enemies[i].transform;
            }
        }
    }

    private void Shoot(int attackNum)
    {
        if (enemyPos == null) return;
        
        if (attackNum == 1)
            selectedObj = arrow;
        if(attackNum == 2)
            selectedObj = fireball;
        
        newAttackObj = Instantiate(selectedObj, transform.position, arrow.transform.rotation);
        newAttackObj.GetComponent<IWeapons>().FindTarget(enemyPos);
    }

    private void ThrowBomb()
    {
        newAttackObj = Instantiate(smokeball, spawnPoint.position, smokeball.transform.rotation);
        weaponInterface = newAttackObj.GetComponent<IWeapons>();
        weaponInterface.FindTarget(enemyPos);
        weaponInterface.Fire();

    }

    public void BuySpinningSword()
    {
        spinningSword.SetActive(true);
        spinningSword.GetComponent<IWeapons>().FindTarget(swordInstance);
        IncreaseSpinSpeed(10);
    }
}