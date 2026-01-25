using UnityEngine;

namespace SB
{
    public class LevelSystem
    {
        private static readonly int[] _expTable = { 50, 75, 125, 150, 2000, 5000 };

        public static void AddExp(Unit unit, int exp)
        {
            unit.Experience += exp;

            while (CheckLevelUp(unit))
            {
                LevelUp(unit);
                Debug.Log($"<color=green>LEVEL UP! New Level: {unit.Level}</color>");
                ApplyRandomBonus(unit);
            }
        }

        public static bool CheckLevelUp(Unit unit)
        {
            if (unit.Level > _expTable.Length) return false;
            return unit.Experience >= GetRequiredExperience(unit.Level);
        }

        public static void LevelUp(Unit unit)
        {
            unit.Experience -= GetRequiredExperience(unit.Level);
            unit.Level++;
        }
        
        private static void ApplyRandomBonus(Unit unit)
        {
            int roll = Random.Range(0, 4);
            string bonusName = "";

            switch (roll)
            {
                case 0:
                    int hpBonus = Mathf.RoundToInt(unit.MaxHealth * 0.1f);
                    unit.CurrentHealth += hpBonus; 
                    bonusName = "+10% HP";
                    break;
                case 1:
                    unit.Speed += unit.Speed * 0.1f;
                    bonusName = "+10% Speed";
                    break;
                case 2:
                    unit.Damage += Mathf.CeilToInt(unit.Damage * 0.1f);
                    bonusName = "+10% Damage";
                    break;
                case 3:
                    unit.FireRate *= 0.9f; 
                    bonusName = "+10% Attack Speed (Cooldown reduced)";
                    break;
            }

            Debug.Log($"<color=green>LEVEL UP! New Level: {unit.Level}. Bonus: {bonusName}</color>");
        }

        public static int GetRequiredExperience(int level)
        {
            return level - 1 < _expTable.Length ? _expTable[level - 1] : int.MaxValue;
        }
        
    }
}