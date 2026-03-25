using UnityEngine;

public class BossFists : MonoBehaviour
{
    [SerializeField] private BossEnemyController bec;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bec.Player().GetComponent<PlayerController>().TakeDamage(bec.Damage());
            bec.Player().GetComponent<DamageIndicator>().TakeDamage();
            bec.ResetFists();
        }
    }
}
