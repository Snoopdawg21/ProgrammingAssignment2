using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private float attackTimer;
    [SerializeField] private float maxFireRate = 2;
    public float fireSpeed = 1;
    public int basicAttackType = 1;
    
    private Transform enemyPos;
    private GameObject[] enemies;
    private float enemyDistance;
    [SerializeField] private Quaternion rotationOffset;

    private GameObject newAttackObj;
    private GameObject selectedObj;

    [Header("Attacks")] 
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject fireball;
    
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

    private void FindClosestEnemy()
    {
        enemyDistance = 0;
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if(enemies == null) return;

        for (int i = 0; i < enemies.Length; i++)
        {
            //pythagoris
            float a = transform.position.x - enemies[i].transform.position.x;
            float b = transform.position.z - enemies[i].transform.position.z;
            float c = (a * a) + (b * b);
            
            float tempDistance = Mathf.Sqrt(c);
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
}