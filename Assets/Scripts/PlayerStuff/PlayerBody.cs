using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private int health = 10;
    
    private void TakeDamage(int amount)
    {
        health -= amount;
        
        Debug.Log(health);
        
        if(health <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Ow.");
            TakeDamage(other.gameObject.GetComponent<EnemyController>().damage);
            Destroy(other.gameObject);
        }
    }
}
