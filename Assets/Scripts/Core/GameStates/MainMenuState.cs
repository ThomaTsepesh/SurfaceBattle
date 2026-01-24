using UnityEngine;


namespace SB
{
    public class MainMenuState : IGameState
    {
        private MainMenu _mainMenuInstance;
        
        public void EnterState()
        {
            InitUi();
        }

        public void ExitState()
        {
            if (_mainMenuInstance != null)
            {
                Object.Destroy(_mainMenuInstance.gameObject);
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

        private void InitUi()
        {
            var prefabMainMenu = Resources.Load<GameObject>("UI/MainMenu");
            if (prefabMainMenu == null)
            {
                Debug.LogError("Could not load MainMenu prefab from Resources/UI/MainMenu");
                return;
            }
            var go = Object.Instantiate(prefabMainMenu);
            _mainMenuInstance = go.GetComponent<MainMenu>();

            _mainMenuInstance.OnStartGameClicked += StartGame;
        }

        private void StartGame()
        {
            GameStateMachine.Instance.ChangeState(new GameState());
        }
    }
}