using UnityEngine;

namespace SB
{
    public static class UnitFactory
    {
        public static Unit CreateEnemy()
        {
            var name = "Cube";
            var hp = 50;
            var speed = 3.0f;

            if (Random.value <= 0.2f)
            {
                name = "Elite Cube";
                hp *= 2;
                speed *= 2;
            }

            return new Unit(name, hp, speed);
        }

        public static Unit CreatePlayer()
        {
            return new Unit(
                name: "Hero",
                health: 100,
                speed: 5.0f
            );
        }
    }
}