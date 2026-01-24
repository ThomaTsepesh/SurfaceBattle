using UnityEngine;

namespace SB
{
    public class LevelContext : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemySpawnPoints;
        [SerializeField] private Camera _levelCamera;

        public Transform PlayerSpawnPoint => _playerSpawnPoint;
        public Transform[] EnemySpawnPoints => _enemySpawnPoints;
        public Camera LevelCamera => _levelCamera;
    }
}