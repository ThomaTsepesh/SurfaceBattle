using UnityEngine;

namespace SB
{
    public interface IGameState
    {
        void EnterState();
        void ExitState();
        void OnUpdate();
        void OnFixedUpdate();
        void ResolveDependencies();
        void ReleaseDependencies();
    }

    public class GameStateMachine : MonoBehaviour
    {
        public static GameStateMachine Instance { get; private set; }

        private IGameState _currentState;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            _currentState?.OnUpdate();
        }

        public void ChangeState(IGameState newState)
        {
            _currentState?.ExitState();
            _currentState = newState;

            Debug.Log("Entering State... " + _currentState.GetType().Name);

            _currentState.ResolveDependencies();
            _currentState.EnterState();
        }
    }
}