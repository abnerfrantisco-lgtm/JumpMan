using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [Header("Spawn")]
    public List<GameObject> obstaclePrefabs;
    public float startDelay = 2f;
    public float spawnInterval = 2f;
    public Transform spawnPoint;

    [Header("Game Over")]
    public bool IsgameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    void SpawnObstacle()
    {
        if (!IsgameOver && obstaclePrefabs.Count > 0)
        {
            int index = Random.Range(0, obstaclePrefabs.Count);
            GameObject obstacleEscolhido = obstaclePrefabs[index];

            Instantiate(obstacleEscolhido,
                spawnPoint.position,
                spawnPoint.rotation);
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle),
            startDelay, spawnInterval);
    }
}
