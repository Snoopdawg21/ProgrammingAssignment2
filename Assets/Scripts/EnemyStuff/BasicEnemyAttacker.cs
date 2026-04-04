using UnityEngine;

public class BasicEnemyAttacker : MonoBehaviour
{
    public int damage;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce;

    void Start()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    public void AttackPlayer()
    {
        rb.AddForce(jumpForce * transform.up + (transform.forward * 2), ForceMode.Impulse);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().TakeDamage(damage);
            player.GetComponent<DamageIndicator>().TakeDamage();
            gameObject.GetComponent<EnemyController>().KillEnemy();
        }
    }
}
