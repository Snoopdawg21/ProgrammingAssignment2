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
        if (spawnTimer > maxTimerCount && enemyCount < 300)
        {
            if (maxTimerCount > 0.1f)
                maxTimerCount -= maxTimerCount / 64;
            
            SpawnEnemy();

            enemyCount++;
            Debug.Log($"Spawned Enemy Numed {enemyCount}");
        }
        
        spawnTimer += Time.deltaTime;
    }

    private void SpawnEnemy()
    {
        if (playerPos == null) return;
        
        spawnOffset = new Vector3(Random.Range(-spawnRange, spawnRange), spawnOffset.y, Random.Range(-spawnRange, spawnRange));
        Instantiate(enemyRef, playerPos.position + spawnOffset, enemyRef.transform.rotation);
        
        spawnTimer = 0;
    }
}
