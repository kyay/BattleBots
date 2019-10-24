using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    public abstract class Vehicle : Player
    {
        public const string WEAPON_CIRCULAR_SAW = "Circular Saw";
        public const string WEAPON_CLAW_CUTTER = "Claw Cutter";
        public const string WEAPON_FLAME_THROWER = "Flame Thrower";
        public const string WEAPON_SLEDGE_HAMMER = "Sledge Hammer";
        public const string WEAPON_SPINNNING_BLADE = "Spinning Blade";


        public int FuelLevel { get; set; } = 50;
        public int ConditionLevel { get; set; } = 50;
        public string Weapon { get; set; }

        public Vehicle()
        {
            Weapon = WEAPON_CIRCULAR_SAW;
            Name = "Ash";
        }

        public Vehicle(string name, string weapon)
        {
            Weapon = weapon;
            Name = name;
        }

        public void HandleDamage(int damage)
        {
            ConditionLevel -= damage;
        }

        public void ConsumeFuel(int fuel)
        {
            FuelLevel -= fuel;
        }

        public abstract void Heal(int amount);
        public abstract void Refuel(int amount);
    }
}
