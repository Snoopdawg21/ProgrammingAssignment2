using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private float attackTimer;
    private Transform enemyPos;

    private GameObject enemies;

    private GameObject newArrow;

    [Header("Attacks")] 
    [SerializeField] private GameObject arrow;
    
    void Update()
    {
        if (attackTimer > 2)
        {
            ShootEnemy();
            attackTimer = 0;
        }
        
        attackTimer += Time.deltaTime;
    }

    private void ShootEnemy()
    {
        enemies = GameObject.FindGameObjectWithTag("Enemy");
        
        if(enemies == null) return;
        
        newArrow = Instantiate(arrow, transform.position, arrow.transform.rotation);
        newArrow.GetComponent<Arrow>().FindTarget(enemies.transform);
    }
    
    

}