using UnityEngine;

public class SpinningSword : MonoBehaviour, IWeapons
{
    [SerializeField] private Transform swordPos;
    private float speed = 10;

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
}
