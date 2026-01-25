using System;
using UnityEngine;

namespace SB
{
    public class UnitCore : MonoBehaviour
    {
        public Unit Data { get; private set; }
        public event Action OnUnitDied = delegate { };
        
        public void Init(Unit model) 
        {
            Data = model;
        }

        public void TakeDamage(int damage)
        {
            Data.CurrentHealth -= damage;
            if (Data.CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnUnitDied.Invoke();
        }
    }
}