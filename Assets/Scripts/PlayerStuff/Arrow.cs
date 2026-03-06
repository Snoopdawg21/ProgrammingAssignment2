using UnityEngine;

public class Arrow : MonoBehaviour, IWeapons
{
    private float speed = 50;

    public void FindTarget(Transform target)
    {
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    public void Fire()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
