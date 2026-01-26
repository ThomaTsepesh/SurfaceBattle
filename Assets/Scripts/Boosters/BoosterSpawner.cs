using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SB
{
    public class BoosterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _boosterPrefab;
        [SerializeField] private Collider _spawnArea;
        [SerializeField] private float _interval = 30f;

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }
        
        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_interval);
                Spawn();
            }
        }

        private void Spawn()
        {
            var bounds = _spawnArea.bounds;
            var position = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                0.5f,
                Random.Range(bounds.min.z, bounds.max.z)
            );
            Instantiate(_boosterPrefab, position, Quaternion.identity);
        }
    }
}