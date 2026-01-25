using UnityEngine;
using UnityEngine.InputSystem;

namespace SB
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Vector2 _vector2;
        private UnitData _unitData;
        private Shooter _shooter;
        private Collider _mapBounds;

        private void Awake()
        {
            _unitData = GetComponent<UnitData>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _shooter = GetComponent<Shooter>();
        }

        public void SetMapBounds(Collider map)
        {
            _mapBounds = map;
            _shooter.Init(0.3f, _mapBounds);
        }

        public void OnMove(InputValue value)
        {
            _vector2 = value.Get<Vector2>();
        }

        public void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _shooter.TryShoot(transform.forward);
            }
        }

        private void FixedUpdate()
        {
            var vector3 = new Vector3(_vector2.x, 0, _vector2.y);
            _rigidbody.MovePosition(_rigidbody.position + vector3 * _unitData.Data.Speed * Time.fixedDeltaTime);

            if (vector3 != Vector3.zero)
            {
                var rotation = Quaternion.LookRotation(vector3);
                _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, rotation, 15f * Time.fixedDeltaTime);
            }
        }
    }
}