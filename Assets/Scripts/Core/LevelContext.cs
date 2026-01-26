using Unity.Cinemachine;
using UnityEngine;

namespace SB
{
    public class LevelContext : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemySpawnPoints;
        [SerializeField] private Camera _levelCamera;
        [SerializeField] private CinemachineCamera _cam;

        public Transform PlayerSpawnPoint => _playerSpawnPoint;
        public Transform[] EnemySpawnPoints => _enemySpawnPoints;
        public Camera LevelCamera => _levelCamera;
        public CinemachineCamera Cam => _cam;
    }
}