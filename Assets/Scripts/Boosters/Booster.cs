using UnityEngine;
using Random = UnityEngine.Random;

namespace SB
{
    public enum BoosterType
    {
        None = 0,
        DoubleDamage,
        DoubleSpeed,
        DoubleXP,
        Invulnerability,
        InstantKillAll
    }

    public class Booster : MonoBehaviour
    {
        [SerializeField] private BoosterType _type;
        [SerializeField] private float _duration = 10f;

        private void Start()
        {
            if (_type == 0)
            {
                _type = (BoosterType)Random.Range(0, 5);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<UnitCore>();
                if (player != null)
                {
                    BoosterSystem.Apply(player, _type, _duration);
                    Destroy(gameObject);
                }
            }
        }
    }
}