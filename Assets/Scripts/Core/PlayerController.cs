using UnityEngine;
using UnityEngine.InputSystem;

namespace SB
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Vector2 _vector2;
        private UnitCore _unitCore;
        private Shooter _shooter;
        private Collider _mapBounds;

        private void Awake()
        {
            _unitCore = GetComponent<UnitCore>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _shooter = GetComponent<Shooter>();
        }

        public void SetMapBounds(Collider map)
        {
            _mapBounds = map;
            _shooter.Init(_mapBounds, _unitCore);
        }

        public void OnMove(InputValue value)
        {
            _vector2 = value.Get<Vector2>();
        }

        public void Update()
        {
            if (Keyboard.current.spaceKey.isPressed)
            {
                _shooter.TryShoot(transform.forward);
            }
        }

        private void FixedUpdate()
        {
            var vector3 = new Vector3(_vector2.x, 0, _vector2.y);
            _rigidbody.MovePosition(_rigidbody.position + vector3 * _unitCore.Data.Speed * Time.fixedDeltaTime);

            if (vector3 != Vector3.zero)
            {
                var rotation = Quaternion.LookRotation(vector3);
                _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, rotation, 15f * Time.fixedDeltaTime);
            }
        }
    }
}