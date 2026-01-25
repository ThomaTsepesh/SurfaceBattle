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

        public bool TakeDamage(int damage)
        {
            Data.CurrentHealth -= damage;
            if (Data.CurrentHealth <= 0)
            {
                Die();
                return true;
            }

            return false;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        public int GetExpReward()
        {
            return Data.RewardExperience;
        }

        private void OnDestroy()
        {
            OnUnitDied.Invoke();
        }
    }
}