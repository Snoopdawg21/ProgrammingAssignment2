using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour, IWeapons
{
    private float speed = 100;
    private int damage = 5;
    private float lifetime;

    public void FindTarget(Transform target)
    {
        transform.LookAt(target);
    }
    
    void Update()
    {
        if(lifetime >= 5)
            Destroy(gameObject);
        
        Fire();
        
        lifetime += Time.deltaTime;
    }

    public void Fire()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);

        if (other.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
