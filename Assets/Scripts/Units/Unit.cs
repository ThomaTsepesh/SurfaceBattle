namespace SB
{
    public class Unit
    {
        public string Name;
        public float Speed;
        
        public int Damage;
        public float FireRate;

        public int MaxHealth;
        public int CurrentHealth;
                
        public int RewardExperience;
        public int Experience = 0;
        public int Level = 1;

        public bool IsInvulnerable;
        public bool IsXpBoosted;

        public Unit(string name, int health, float speed)
        {
            Name = name;
            MaxHealth = health;
            Damage = 25;
            FireRate = 0.3f;
            CurrentHealth = health;
            Speed = speed;
            RewardExperience = health / 2;
        }
    }
}