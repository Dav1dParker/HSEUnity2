using _Pr2.Scripts.Objects;
using UnityEngine;

namespace _Pr2.Scripts.Core
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private Spike spikePrefab;
        [SerializeField] private Transform despawnPoint;
        [SerializeField] private float minSpawnInterval = 1.15f;
        [SerializeField] private float maxSpawnInterval = 2.15f;

        private float spawnTimer;

        private void Start()
        {
            spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        private void Update()
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer > 0f)
            {
                return;
            }

            var spike = Instantiate(spikePrefab, transform.position, Quaternion.identity);
            spike.SetDespawnX(despawnPoint ? despawnPoint.position.x : float.NegativeInfinity);
            spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
}
