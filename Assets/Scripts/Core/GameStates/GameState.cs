using System.Threading.Tasks;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SB
{
    public class GameState: IGameState
    {
        private const string _sceneName = "BattleLevel";
        
        private LevelContext _levelContext;
        private GameObject _playerInstance;
        
        public async void EnterState()
        {
            var loading = SceneManager.LoadSceneAsync(_sceneName);
            while (!loading.isDone) await Task.Yield();
            
            _levelContext = Object.FindAnyObjectByType<LevelContext>();
            
            InitLevel();
        }

        public void ExitState()
        {
            if (_playerInstance != null) 
                Object.Destroy(_playerInstance);
        }

        public void OnUpdate()
        {
            
        }

        public void OnFixedUpdate()
        {
            
        }

        public void ResolveDependencies()
        {
            
        }

        public void ReleaseDependencies()
        {
            
        }

        private void InitLevel()
        {
            var playerPrefab = Resources.Load<GameObject>("Prefabs/player");
            _playerInstance = Object.Instantiate(
                    playerPrefab, 
                    _levelContext.PlayerSpawnPoint.position, 
                    _levelContext.PlayerSpawnPoint.rotation
                );
            
            _playerInstance.name = "Hero";
            
            var playerModel = UnitFactory.CreatePlayer();
            _playerInstance.GetComponent<UnitData>().Init(playerModel);
            
            var cameraCtrl = _levelContext.Cam.GetComponent<CameraController>();
            cameraCtrl.SetTarget(_playerInstance.transform);

            var spawner = Object.FindAnyObjectByType<EnemySpawner>();
            spawner.StartSpawn();
        }
    }
}