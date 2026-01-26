using UnityEngine;

namespace SB
{
    public class EnemyAI : MonoBehaviour
    {
        //TODO - выкинуть иишный иишник
        private UnitCore _core;
        private Transform _target;
        private Collider _mapBounds;
        
        // Параметры поведения
        private float _preferredDistance = 5f; // Предпочитаемая дистанция до игрока
        private float _orbitAngle; // Угол для движения по кругу
        private float _orbitSpeed = 30f; // Скорость вращения вокруг игрока (градусы/сек)
        
        private void Awake()
        {
            _core = GetComponent<UnitCore>();
            // Каждый враг получает случайный начальный угол
            _orbitAngle = Random.Range(0f, 360f);
            // Разброс дистанции, чтобы враги не сбивались в кучу
            _preferredDistance = Random.Range(4f, 7f);
        }

        public void Initialize(Transform target, Collider mapBounds)
        {
            _target = target;
            _mapBounds = mapBounds;
        }

        private void Update()
        {
            if (_target == null || _core.Data == null) return;

            MoveWithTactics();
        }

        private void MoveWithTactics()
        {
            Vector3 toTarget = _target.position - transform.position;
            float currentDistance = toTarget.magnitude;
            
            Vector3 moveDirection = Vector3.zero;
            
            // Если слишком близко - отходим
            if (currentDistance < _preferredDistance - 1f)
            {
                moveDirection = -toTarget.normalized;
            }
            // Если слишком далеко - подходим
            else if (currentDistance > _preferredDistance + 1f)
            {
                moveDirection = toTarget.normalized;
            }
            // На оптимальной дистанции - двигаемся по кругу
            else
            {
                _orbitAngle += _orbitSpeed * Time.deltaTime;
                if (_orbitAngle > 360f) _orbitAngle -= 360f;
                
                // Вычисляем точку на окружности вокруг игрока
                Vector3 offset = Quaternion.Euler(0, _orbitAngle, 0) * Vector3.forward * _preferredDistance;
                Vector3 orbitPoint = _target.position + offset;
                
                moveDirection = (orbitPoint - transform.position).normalized;
            }
            
            // Добавляем немного случайности в движение
            Vector3 randomOffset = new Vector3(
                Mathf.PerlinNoise(Time.time * 0.5f, transform.position.z) - 0.5f,
                0,
                Mathf.PerlinNoise(Time.time * 0.5f, transform.position.x) - 0.5f
            ) * 0.3f;
            
            moveDirection = (moveDirection + randomOffset).normalized;
            
            // Новая позиция
            Vector3 newPosition = transform.position + moveDirection * _core.Data.Speed * Time.deltaTime;
            
            // Ограничиваем движение границами карты
            newPosition = ClampToMapBounds(newPosition);
            
            // Применяем движение
            transform.position = newPosition;
            
            // Поворачиваем моба лицом к игроку
            Vector3 lookDirection = (_target.position - transform.position);
            lookDirection.y = 0;
            
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, 
                    Quaternion.LookRotation(lookDirection), 
                    Time.deltaTime * 5f
                );
            }
        }

        private Vector3 ClampToMapBounds(Vector3 position)
        {
            if (_mapBounds == null) return position;
            
            Bounds bounds = _mapBounds.bounds;
            
            position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
            position.z = Mathf.Clamp(position.z, bounds.min.z, bounds.max.z);
            
            return position;
        }
    }
}