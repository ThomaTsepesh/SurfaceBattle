using UnityEngine;

namespace SB
{
    public class Shooter : MonoBehaviour
    {
        private GameObject _bulletPrefab;
        private UnitCore _unitCore;
        private float _nextFireTime;
        private Collider _mapBounds;
        
        private void Awake()
        {
            _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        }

        public void Init(Collider mapBounds, UnitCore core)
        {
            _mapBounds = mapBounds;
            _unitCore = core;
        }

        public void TryShoot(Vector3 direction)
        {
            if (Time.time < _nextFireTime) return;
            
            Shoot(direction);
            _nextFireTime = Time.time + _unitCore.Data.FireRate;
        }


        private void Shoot(Vector3 direction)
        {
            var spawnPos = transform.position + Vector3.up * 0.1f;
            var bullet = Instantiate(_bulletPrefab, spawnPos, Quaternion.identity);
            
            bullet.GetComponent<Bullet>().Init(
                direction, 
                _unitCore.Data.Damage,
                _unitCore, 
                _mapBounds
            );
        }
    }
}