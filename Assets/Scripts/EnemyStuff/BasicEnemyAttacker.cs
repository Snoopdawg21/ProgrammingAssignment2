using UnityEngine;

public class BasicEnemyAttacker : MonoBehaviour
{
    public int damage;
    [SerializeField] private GameObject player;

    void Start()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ow");
            player.GetComponent<PlayerController>().TakeDamage(damage);
            player.GetComponent<DamageIndicator>().TakeDamage();
            gameObject.GetComponent<EnemyController>().KillEnemy();
        }
    }
}
