using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private float attackTimer;
    public float maxFireRate = 2;
    public float fireSpeed = 1;
    private Transform enemyPos;
    private GameObject[] enemies;
    private float enemyDistance;

    private GameObject newArrow;

    [Header("Attacks")] 
    [SerializeField] private GameObject arrow;
    
    void Update()
    {
        if (attackTimer > maxFireRate)
        {
            ShootEnemy();
            attackTimer = 0;
        }

        attackTimer += Time.deltaTime * fireSpeed;
    }

    private void ShootEnemy()
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
        
        Debug.Log($"enemy is {enemyDistance} spaces away");
        
        newArrow = Instantiate(arrow, transform.position, arrow.transform.rotation);
        newArrow.GetComponent<Arrow>().FindTarget(enemyPos);
    }
    
    

}