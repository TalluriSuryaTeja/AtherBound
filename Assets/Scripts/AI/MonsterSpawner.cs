using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class MonsterSpawnInfo
    {
        public MonsterData monsterData;
        public int count = 1;
    }

    [Header("Spawner Settings")]
    public List<MonsterSpawnInfo> monstersToSpawn;
    public float spawnRadius = 5f;
    public float respawnTime = 30f; // Time in seconds before respawning
    public bool spawnOnStart = true;

    private List<GameObject> spawnedMonsters = new List<GameObject>();
    private bool isRespawning = false;

    void Start()
    {
        if (spawnOnStart)
        {
            SpawnMonsters();
        }
    }

    void Update()
    {
        // Simple check to see if monsters have been defeated
        spawnedMonsters.RemoveAll(item => item == null);

        if (!isRespawning && spawnedMonsters.Count == 0)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    private void SpawnMonsters()
    {
        if (monstersToSpawn.Count == 0) return;
        isRespawning = false;

        foreach (var spawnInfo in monstersToSpawn)
        {
            for (int i = 0; i < spawnInfo.count; i++)
            {
                // Find a random position within the spawn radius on the NavMesh
                Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
                randomPos += transform.position;

                if (UnityEngine.AI.NavMesh.SamplePosition(randomPos, out UnityEngine.AI.NavMeshHit hit, spawnRadius, 1))
                {
                    GameObject monsterPrefab = spawnInfo.monsterData.monsterPrefab;
                    if (monsterPrefab != null)
                    {
                        GameObject newMonster = Instantiate(monsterPrefab, hit.position, Quaternion.identity);
                        newMonster.name = $"{spawnInfo.monsterData.monsterName}_{i}";
                        spawnedMonsters.Add(newMonster);
                    }
                    else
                    {
                        Debug.LogWarning($"Prefab for {spawnInfo.monsterData.name} is not set!", this);
                    }
                }
                else
                {
                    Debug.LogWarning("Could not find a valid position on the NavMesh to spawn a monster.", this);
                }
            }
        }
    }

    private IEnumerator RespawnCoroutine()
    {        
        isRespawning = true;
        yield return new WaitForSeconds(respawnTime);
        SpawnMonsters();
    }

    // Visualize the spawn radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
