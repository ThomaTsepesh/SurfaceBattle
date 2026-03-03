using UnityEngine;
using UnityEngine.UIElements;

namespace SB
{
    public class HealthBar : MonoBehaviour
    {
        private VisualElement _fill;
        private Transform _camTransform;
        private UIDocument _uiDocument;
        private Label _damageLabel;

        private void Awake()
        {
            if (Camera.main != null)
                _camTransform = Camera.main.transform;
            
            _uiDocument = GetComponent<UIDocument>();
            var root = _uiDocument.rootVisualElement;
            _fill = root.Q<VisualElement>("health-fill");
            _damageLabel = root.Q<Label>("damage-text");
        }

        public void LateUpdate()
        {
            if (_camTransform == null) return;
            
            transform.LookAt(transform.position + _camTransform.rotation * Vector3.forward,
                _camTransform.rotation * Vector3.up);
        }
        
        public void ShowDamage(int amount)
        {
            if (_damageLabel == null) return;

            _damageLabel.text = $"-{amount}";
            
            _damageLabel.style.opacity = 1;
            _damageLabel.style.top = -40;

            _damageLabel.schedule.Execute(() => 
            {
                _damageLabel.style.opacity = 0;
                _damageLabel.style.top = -20;
            }).StartingIn(800); 
        }

        public void SetProgress(float percentage)
        {
            if (_fill != null)
                _fill.style.width = Length.Percent(Mathf.Clamp(percentage, 0, 100));
        }
    }
}