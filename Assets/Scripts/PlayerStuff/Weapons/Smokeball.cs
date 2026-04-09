using System.Collections;
using UnityEngine;

public class Smokeball : MonoBehaviour, IWeapons
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AttackManager am;
    [SerializeField] private GameObject smokeCloud;
    
    void Start()
    {
        if (am == null)
            am = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackManager>();
        
        StartCoroutine(Lifetime(5));
    }

    private IEnumerator Lifetime(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    public void Fire()
    {
        rb.AddForce((transform.forward * am.throwForce) + am.throwHeight, ForceMode.Impulse);
    }

    public void FindTarget(Transform target)
    {
        if(target == null)
            Destroy(gameObject);
        
        transform.LookAt(target);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Instantiate(smokeCloud, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
