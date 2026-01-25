using UnityEngine;

namespace SB
{
    public class Bullet : MonoBehaviour
    {
        private float _speed = 30f;
        private int _damage;
        private string _ownerTag;
        private Rigidbody _rigidbody;
        private Collider _mapBounds;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Vector3 direction, int damage, string ownerTag, Collider mapBounds)
        {
            _damage = damage;
            _ownerTag = ownerTag;
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
            if (other.CompareTag(_ownerTag))
            {
                return;
            }

            var target = other.GetComponent<UnitData>();
            if (target != null)
            {
                target.Data.CurrentHealth -= _damage;
                Debug.Log($"Попал в {other.name}! HP осталось: {target.Data.CurrentHealth}");
                Destroy(gameObject);
            }
        }
    }
}