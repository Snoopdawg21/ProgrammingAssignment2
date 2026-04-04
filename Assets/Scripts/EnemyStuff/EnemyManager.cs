using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyRef;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector3 spawnOffset;
    [SerializeField] private float spawnRange;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float maxTimerCount;
    public int enemyCount;

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer > maxTimerCount && enemyCount < 150)
        {
            if (maxTimerCount > 0.5f)
                maxTimerCount -= maxTimerCount / 64;
            
            SpawnEnemy();

            enemyCount++;
        }
        
        spawnTimer += Time.deltaTime;
    }

    public void SpawnEnemy()
    {
        spawnOffset = new Vector3(Random.Range(-spawnRange, spawnRange), spawnOffset.y, Random.Range(-spawnRange, spawnRange));
        Instantiate(enemyRef, spawnOffset, enemyRef.transform.rotation);
        
        spawnTimer = 0;
    }
}
