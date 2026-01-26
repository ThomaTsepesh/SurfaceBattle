using UnityEngine;

namespace SB
{
    public class Bullet : MonoBehaviour
    {
        private float _speed = 30f;
        private int _damage;
        private UnitCore _owner;
        private Rigidbody _rigidbody;
        private Collider _mapBounds;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Vector3 direction, int damage, UnitCore owner, Collider mapBounds)
        {
            _damage = damage;
            _owner = owner;
            _mapBounds = mapBounds;

            _rigidbody.linearVelocity = new Vector3(direction.x, 0, direction.z).normalized * _speed;

            Destroy(gameObject, 5f);
        }

        private void FixedUpdate()
        {
            if (_mapBounds != null && !_mapBounds.bounds.Contains(transform.position))
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_owner.tag))
            {
                return;
            }

            var target = other.GetComponent<UnitCore>();
            if (target != null)
            {
                if (target.TakeDamage(_damage))
                {
                    LevelSystem.AddExp(_owner.Data, target.GetExpReward());
                }
                Destroy(gameObject);
            }
        }
    }
}