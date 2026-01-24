namespace SB
{
    public class Unit
    {
        public string Name { get; }
        public int MaxHealth { get; }
        public float Speed { get; }
        public int RewardExperience { get; }
        public int CurrentHealth { get; set; }

        public Unit(string name, int health, float speed)
        {
            Name = name;
            MaxHealth = health;
            CurrentHealth = health;
            Speed = speed;
            RewardExperience = health / 2;
        }
    }
}