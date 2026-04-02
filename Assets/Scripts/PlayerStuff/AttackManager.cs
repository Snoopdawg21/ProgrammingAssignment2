using Unity.VisualScripting;
using UnityEngine;

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
    [SerializeField] private GameObject spinningSword;
    [SerializeField] private Transform swordInstance;
    
    [Space(10)]
    [Header("Basic Attack Stats")]
    [SerializeField] private float attackTimer;
    [SerializeField] private float maxFireRate = 3;
    public float fireSpeed = 1;
    public int basicAttackType = 1;

    [Space(10)] [Header("Spinning Sword Stats")] 
    [SerializeField] private float spinSpeed;

    private float a;
    private float b;
    private float c;
    private float tempDistance;

    void Start()
    {
        attackTimer = 0;
    }

    void Update()
    {
        if (attackTimer > maxFireRate)
        {
            FindClosestEnemy();
            Shoot(basicAttackType);
            attackTimer = 0;
        }

        attackTimer += Time.deltaTime * fireSpeed;
    }

    public void IncreaseSpinSpeed(float amount)
    {
        spinSpeed += amount;
        spinningSword.GameObject().GetComponent<SpinningSword>().GetSpeed(spinSpeed);
    }

    private void FindClosestEnemy()
    {
        enemyDistance = 0;
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if(enemies == null) return;

        if (enemies.Length == 1)
        {
            if(enemies[0].gameObject.GetComponent<EnemyController>().enabled)
                enemyPos = enemies[0].transform;
            
            return;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null) return;
            
            if (enemies[i].gameObject.GetComponent<EnemyController>().enabled == false)
            {
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

    public void BuySpinningSword()
    {
        spinningSword.SetActive(true);
        spinningSword.GameObject().GetComponent<IWeapons>().FindTarget(swordInstance);
        IncreaseSpinSpeed(10);
    }
}