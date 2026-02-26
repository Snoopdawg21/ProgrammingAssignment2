using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private float attackTimer;
    private Transform enemyPos;

    private GameObject[] enemies;

    [Header("Attacks")] 
    [SerializeField] private GameObject arrow;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
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
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 newPos = enemies[i].transform.position;

            if (transform.position.magnitude - newPos.magnitude <
                transform.position.magnitude - enemyPos.position.magnitude)
                enemyPos.position = newPos;
        }
        
        GameObject newArrow = Instantiate(arrow, transform.position, arrow.transform.rotation);
        newArrow.GetComponent<Arrow>().FindTarget(enemyPos);
    }
}