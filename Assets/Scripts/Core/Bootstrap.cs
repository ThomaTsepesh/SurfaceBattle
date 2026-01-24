using UnityEngine;

namespace SB
{
    public class Bootstrap: MonoBehaviour
    {
        [SerializeField] private GameStateMachine _gameStateMachinePrefab;
        
        private void Start()
        {
            if (_gameStateMachinePrefab == null)
            {
                return;
            }
            
            GameStateMachine stateMachine = Instantiate(_gameStateMachinePrefab);

            stateMachine.ChangeState(new MainMenuState());
        }
    }
}