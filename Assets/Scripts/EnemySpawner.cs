using UnityEngine;
using System.Collections;

namespace SB
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Collider _boxCollider;

        [SerializeField] private float _spawnInterval = 2f;
        [SerializeField] private float _offsetFromEdge = 1f;

        private bool _isSpawning;

        public void StartSpawn()
        {
            _isSpawning = true;
            StartCoroutine(SpawnRoutine());
        }
        private IEnumerator SpawnRoutine()
        {
            while (_isSpawning)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private void SpawnEnemy()
        {
            var spawnPos = GetRandomPointOnEdge();
            var enemyObj = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            var model = UnitFactory.CreateEnemy();
            var core = enemyObj.GetComponent<UnitData>();
            if (core != null)
                core.Init(model);
        }

        private Vector3 GetRandomPointOnEdge()
        {
            Bounds bounds = _boxCollider.bounds;
            
            var side = Random.Range(0, 4);
            var x = 0f;
            var z = 0f;

            switch (side)
            {
                case 0:
                    x = Random.Range(bounds.min.x, bounds.max.x);
                    z = bounds.max.z - _offsetFromEdge;
                    break;
                case 1:
                    x = Random.Range(bounds.min.x, bounds.max.x);
                    z = bounds.min.z + _offsetFromEdge;
                    break;
                case 2:
                    x = bounds.min.x + _offsetFromEdge;
                    z = Random.Range(bounds.min.z, bounds.max.z);
                    break;
                case 3:
                    x = bounds.max.x - _offsetFromEdge;
                    z = Random.Range(bounds.min.z, bounds.max.z);
                    break;
            }

            return new Vector3(x, 0.5f, z);
        }
    }
}