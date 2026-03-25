using UnityEngine;

public class Arrow : MonoBehaviour, IWeapons
{
    private float speed = 50;
    private int damage = 1;
    private float lifeTime;
    
    public void FindTarget(Transform target)
    {
        if (target == null)
            Destroy(gameObject);
        
        transform.up = -1 * (target.position - transform.position);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(lifeTime >= 3)
            Destroy(gameObject);
        
        Fire();
        lifeTime += Time.deltaTime;
    }

    public void Fire()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other?.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            
            Destroy(gameObject);
        }
    }
    

}
