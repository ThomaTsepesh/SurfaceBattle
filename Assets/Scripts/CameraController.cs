using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SB
{
    public class CameraController : MonoBehaviour
    {
        private CinemachineCamera _cam;
        private CinemachineFollow _follow;
        
        [SerializeField] private float _zoomSpeed = 5f;
        [SerializeField] private float _minHeight = 5f;
        [SerializeField] private float _maxHeight = 25f;
        
        private void Awake()
        {
            _cam = GetComponent<CinemachineCamera>();
            _follow = GetComponent<CinemachineFollow>();
        }

        private void Update()
        {
            float scroll = Mouse.current.scroll.ReadValue().y * 0.01f; 
            
            if (scroll != 0 && _follow != null)
            {
                ApplyZoom(scroll);
            }
        }

        private void ApplyZoom(float scroll)
        {
            var currentOffset = _follow.FollowOffset;
            var targetY = Mathf.Clamp(currentOffset.y - scroll * _zoomSpeed * 10f, _minHeight, _maxHeight);
            
            _follow.FollowOffset = new Vector3(currentOffset.x, targetY, -targetY);
        }
        
        public void SetTarget(Transform target)
        {
            _cam.Follow = target;
            _cam.LookAt = target;
        }
    }
}