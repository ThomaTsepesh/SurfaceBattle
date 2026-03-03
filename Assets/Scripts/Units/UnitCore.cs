using System;
using UnityEngine;

namespace SB
{
    public class UnitCore : MonoBehaviour
    {
        public HealthBar _hpBar;
        public Unit Data { get; private set; }
        public event Action OnUnitDied = delegate { };
        
        public void Init(Unit model) 
        {
            Data = model;
        }

        public void SetHpBar(HealthBar hpBar)
        {
            _hpBar = hpBar;
        }

        public bool TakeDamage(int damage)
        {
            if (Data.IsInvulnerable)
            {
                return false;
            }
            
            Data.CurrentHealth -= damage;
            _hpBar.SetProgress(Data.CurrentHealth);
            _hpBar.ShowDamage(damage);
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