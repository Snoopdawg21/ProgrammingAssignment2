using System.Collections;
using UnityEngine;

public class SmokeCloud : MonoBehaviour
{
    private GameObject currentEnemy;
    private EnemyMovement em;

    void Start()
    {
        StartCoroutine(Lifetime(5));
    }

    private IEnumerator Lifetime(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other.gameObject.name);
            currentEnemy = other.gameObject;

            if (!currentEnemy.GetComponent<EnemyMovement>()) return;

            em = currentEnemy.GetComponent<EnemyMovement>();
            
            if(em.CurrentState() == EnemyStates.Chase)
                em.SwitchState(EnemyStates.Paused);
        }
    }
}
