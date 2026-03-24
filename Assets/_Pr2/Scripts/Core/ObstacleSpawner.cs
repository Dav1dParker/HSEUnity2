using _Pr2.Scripts.Objects;
using UnityEngine;

namespace _Pr2.Scripts.Core
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private ScrollingObject objectPrefab;
        [SerializeField] private Transform despawnPoint;
        [SerializeField] private float minSpawnInterval = 1.15f;
        [SerializeField] private float maxSpawnInterval = 2.15f;
        [SerializeField] private float minObjectSpeed = 5.5f;
        [SerializeField] private float maxObjectSpeed = 8f;
        [SerializeField] private float objectSpacing = 1.25f;
        [SerializeField] private bool speedUpWithTime;
        [SerializeField] private float speedIncreasePerSecond = 0.1f;
        [SerializeField] private float maxSpeed = 12f;

        private float spawnTimer;
        private float elapsedTime;

        private void Start()
        {
            spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        private void Update()
        {
            if (speedUpWithTime)
            {
                elapsedTime += Time.deltaTime;
            }

            spawnTimer -= Time.deltaTime;
            if (spawnTimer > 0f)
            {
                return;
            }

            SpawnWave();
            spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        private void SpawnWave()
        {
            var despawnX = despawnPoint ? despawnPoint.position.x : float.NegativeInfinity;
            var objectCount = Random.Range(1, 4);
            var moveSpeed = Random.Range(minObjectSpeed, maxObjectSpeed);

            if (speedUpWithTime)
            {
                moveSpeed = Mathf.Min(moveSpeed + elapsedTime * speedIncreasePerSecond, maxSpeed);
            }

            for (var i = 0; i < objectCount; i++)
            {
                var spawnPosition = transform.position + Vector3.right * (i * objectSpacing);
                var spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
                spawnedObject.SetDespawnX(despawnX);
                spawnedObject.SetMoveSpeed(moveSpeed);
            }
        }
    }
}
