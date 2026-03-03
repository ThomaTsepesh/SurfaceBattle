using UnityEngine;
using UnityEngine.UIElements;

namespace SB
{
    public class WorldTooltip : MonoBehaviour
    {
        private Transform _camTransform;
        private VisualElement _root;
        
        private void Awake()
        {
            if (Camera.main != null)
                _camTransform = Camera.main.transform;
            
            _root = GetComponent<UIDocument>().rootVisualElement;
            Hide();
        }
        
        public void LateUpdate()
        {
            if (_camTransform == null) return;
            
            transform.LookAt(transform.position + _camTransform.rotation * Vector3.forward,
                _camTransform.rotation * Vector3.up);
        }
        
        public void Show() => _root.style.display = DisplayStyle.Flex;
        public void Hide() => _root.style.display = DisplayStyle.None;
    }
}