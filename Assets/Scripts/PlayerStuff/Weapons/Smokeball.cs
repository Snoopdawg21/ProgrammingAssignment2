using System.Collections;
using UnityEngine;

public class Smokeball : MonoBehaviour, IWeapons
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 throwHeight;
    [SerializeField] private float throwForce;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Lifetime(5));
    }

    private IEnumerator Lifetime(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
    
    public void Fire()
    {
        rb.AddForce((transform.forward * throwForce) + throwHeight);
    }

    public void FindTarget(Transform target)
    {
        if(target == null)
            Destroy(gameObject);
        
        transform.LookAt(target);
    }
}
