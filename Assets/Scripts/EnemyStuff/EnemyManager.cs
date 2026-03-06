using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyRef;
    [SerializeField] private Vector3 spawnOffset;
    [SerializeField] private float spawnRange;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float maxTimerCount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer > maxTimerCount)
            SpawnEnemy();
        
        spawnTimer += Time.deltaTime;
    }

    private void SpawnEnemy()
    {
        spawnOffset = new Vector3(Random.Range(-spawnRange, spawnRange), 5, Random.Range(-spawnRange, spawnRange));
        Instantiate(enemyRef, transform.position + spawnOffset, enemyRef.transform.rotation);
        
        spawnTimer = 0;
    }
}
