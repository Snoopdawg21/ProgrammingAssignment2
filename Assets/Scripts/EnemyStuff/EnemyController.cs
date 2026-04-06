using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float gravity;

    [SerializeField] private GameManager gm;
    [SerializeField] private EnemyManager eManager;
    
    private float lifetime;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private bool despawnable;
    
    void Start()
    {
        health = maxHealth;
        
        if(gm == null)
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        if(eManager == null)
            eManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemyManager>();
    }
    
    void Update()
    {
        if (lifetime > maxLifeTime)
        {
            Debug.Log("Timed Out");
            eManager.SpawnEnemy();
            KillEnemy();
        }
        
        if (health == maxHealth && despawnable)
        {
            lifetime += Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        
        gm.IncreaseScore(maxHealth - health);

        if (health <= 0)
            KillEnemy();
    }

    public void KillEnemy()
    {
        if (eManager == null)
            eManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemyManager>();

        eManager.enemyCount--;
        
        Destroy(gameObject);
    }

    public void DespawnToggle()
    {
        despawnable = !despawnable;
    }
}
