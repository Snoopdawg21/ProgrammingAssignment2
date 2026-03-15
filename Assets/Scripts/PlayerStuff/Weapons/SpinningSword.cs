using UnityEngine;

public class SpinningSword : MonoBehaviour, IWeapons
{
    private Transform swordPos;
    private float speed;
    private int damage = 5;

    public void GetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
    void Update()
    {
        Fire();
    }

    public void FindTarget(Transform target)
    {
        swordPos = target;
    }

    public void Fire()
    {
        swordPos.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Debug.LogWarning("Hit Enemy");
        }
    }
}
