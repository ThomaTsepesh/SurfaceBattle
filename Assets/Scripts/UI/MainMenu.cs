using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace SB
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        public event Action OnStartGameClicked = delegate { };

        private void OnEnable()
        {
            var root = _uiDocument.rootVisualElement;

            var startButton = root.Q<Button>("start-btn");
            if (startButton != null)
            {
                startButton.clicked += () => OnStartGameClicked?.Invoke();
            }
        }
    }
}