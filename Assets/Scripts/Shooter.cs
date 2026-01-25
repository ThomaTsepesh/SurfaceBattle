using UnityEngine;

namespace SB
{
    public class Shooter : MonoBehaviour
    {
        private GameObject _bulletPrefab;
        private float _fireRate;
        private float _nextFireTime;
        private Collider _mapBounds;
        
        private void Awake()
        {
            _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        }

        public void Init(float fireRate, Collider mapBounds)
        {
            _fireRate = fireRate;
            _mapBounds = mapBounds;
        }

        public void TryShoot(Vector3 direction)
        {
            if (Time.time < _nextFireTime) return;
            
            Shoot(direction);
            _nextFireTime = Time.time + _fireRate;
        }


        private void Shoot(Vector3 direction)
        {
            var spawnPos = transform.position + Vector3.up * 0.1f;
            var bullet = Instantiate(_bulletPrefab, spawnPos, Quaternion.identity);
            
            bullet.GetComponent<Bullet>().Init(
                direction, 
                25, 
                gameObject.tag, 
                _mapBounds
            );
        }
    }
}