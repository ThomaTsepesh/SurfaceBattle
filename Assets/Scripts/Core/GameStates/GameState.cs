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
        private static GameUi _gameUiInstance;
        
        public async void EnterState()
        {
            var loading = SceneManager.LoadSceneAsync(_sceneName);
            while (!loading.isDone) await Task.Yield();
            
            _levelContext = Object.FindAnyObjectByType<LevelContext>();
            
            InitUi();
            InitLevel();
        }

        public void ExitState()
        {
            if (_playerInstance != null) 
                Object.Destroy(_playerInstance);
            
            if (_gameUiInstance != null)
            {
                Object.Destroy(_gameUiInstance.gameObject);
            }
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
            
            var hpPrefab = Resources.Load<GameObject>("UI/HealthBarWorld");
            var hpObj = Object.Instantiate(hpPrefab, _playerInstance.transform);
            hpObj.transform.localPosition = new Vector3(0, 1.5f, 0);
            var healthBarComponent = hpObj.GetComponent<HealthBar>();
            _playerInstance.GetComponent<UnitCore>().SetHpBar(healthBarComponent);
            _playerInstance.name = "Hero";
            
            var playerModel = UnitFactory.CreatePlayer();
            _playerInstance.GetComponent<UnitCore>().Init(playerModel);
            
            var cameraCtrl = _levelContext.Cam.GetComponent<CameraController>();
            cameraCtrl.SetTarget(_playerInstance.transform);

            var spawner = Object.FindAnyObjectByType<EnemySpawner>();
            spawner.StartSpawn(_playerInstance.transform);
            _playerInstance.GetComponent<PlayerController>().SetMapBounds(spawner.GetComponent<Collider>()); 
        }

        private void InitUi()
        {
            var prefabMainMenu = Resources.Load<GameObject>("UI/GameUi");
            if (prefabMainMenu == null)
            {
                Debug.LogError("Could not load GameUi prefab from Resources/UI/GameUi");
                return;
            }
            var go = Object.Instantiate(prefabMainMenu);
            _gameUiInstance = go.GetComponent<GameUi>();
        }
    }
}