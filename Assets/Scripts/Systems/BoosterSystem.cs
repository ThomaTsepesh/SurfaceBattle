using System.Collections;
using UnityEngine;

namespace SB
{
    public static class BoosterSystem
    {
        public static void Apply(UnitCore player, BoosterType type, float duration)
        {
            player.StartCoroutine(BoosterRoutine(player, type, duration));
        }

        private static IEnumerator BoosterRoutine(UnitCore player, BoosterType type, float duration)
        {
            var data = player.Data;
            switch (type)
            {
                case BoosterType.DoubleDamage:
                    data.Damage *= 2;
                    yield return new WaitForSeconds(duration);
                    data.Damage /= 2;
                    break;

                case BoosterType.DoubleSpeed:
                    data.Speed *= 2;
                    yield return new WaitForSeconds(duration);
                    data.Speed /= 2;
                    break;

                case BoosterType.DoubleXP:
                    data.IsXpBoosted = true;
                    yield return new WaitForSeconds(duration);
                    data.IsXpBoosted = false;
                    break;

                case BoosterType.Invulnerability:
                    data.IsInvulnerable = true;
                    yield return new WaitForSeconds(duration);
                    data.IsInvulnerable = false;
                    break;

                case BoosterType.InstantKillAll:
                    InstantKillAll();
                    break;
            }
        }

        private static void InstantKillAll()
        {
            //TODO: чет мне не нравится, мб переделаю
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                var data = enemy.GetComponent<UnitCore>();
                if (data != null) data.TakeDamage(999999); 
            }
        }
    }
}