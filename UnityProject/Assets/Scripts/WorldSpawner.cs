using UnityEngine;

namespace Gamedoido
{
    public class WorldSpawner : MonoBehaviour
    {
        public Transform[] crateSpawnPoints;
        public Transform[] enemySpawnPoints;

        public GameObject cratePrefab;
        public GameObject humanEnemyPrefab;
        public GameObject zombieEnemyPrefab;
        public GameObject bossEnemyPrefab;

        [Range(0f, 1f)]
        public float bossSpawnChance = 0.1f;

        public void SpawnWorld()
        {
            SpawnCrates();
            SpawnEnemies();
        }

        private void SpawnCrates()
        {
            if (cratePrefab == null)
            {
                return;
            }

            foreach (Transform point in crateSpawnPoints)
            {
                if (point == null)
                {
                    continue;
                }

                Instantiate(cratePrefab, point.position, point.rotation, transform);
            }
        }

        private void SpawnEnemies()
        {
            foreach (Transform point in enemySpawnPoints)
            {
                if (point == null)
                {
                    continue;
                }

                GameObject prefab = Random.value < bossSpawnChance ? bossEnemyPrefab : ChooseStandardEnemy();
                if (prefab != null)
                {
                    Instantiate(prefab, point.position, point.rotation, transform);
                }
            }
        }

        private GameObject ChooseStandardEnemy()
        {
            if (humanEnemyPrefab == null && zombieEnemyPrefab == null)
            {
                return null;
            }

            if (humanEnemyPrefab == null)
            {
                return zombieEnemyPrefab;
            }

            if (zombieEnemyPrefab == null)
            {
                return humanEnemyPrefab;
            }

            return Random.value > 0.5f ? humanEnemyPrefab : zombieEnemyPrefab;
        }
    }
}
