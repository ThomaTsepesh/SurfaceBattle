using UnityEngine;
using UnityEngine.InputSystem;

namespace SB
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        private Rigidbody _rigidbody;
        private Vector2 _vector2;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        public void OnMove(InputValue value)
        {
            _vector2 = value.Get<Vector2>();
        }

        private void FixedUpdate()
        {
            var vector3 = new Vector3(_vector2.x, 0, _vector2.y);
            _rigidbody.MovePosition(_rigidbody.position + vector3 * _moveSpeed * Time.fixedDeltaTime);
        }
    }
}