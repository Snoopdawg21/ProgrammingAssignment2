using UnityEngine;

public class SpinSwordHit : MonoBehaviour
{
    private int damage = 5;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Debug.LogWarning("Hit Enemy");
        }
    }
}
