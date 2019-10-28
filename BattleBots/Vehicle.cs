using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    public abstract class Vehicle : Player
    {
        public int FuelLevel { get; set; } = 75;
        public int ConditionLevel { get; set; } = 50;
        public string Weapon { get; set; }

        public Vehicle()
        {
            Weapon = Game.WEAPON_CIRCULAR_SAW;
            Name = "Ash";
        }

        public Vehicle(string weapon)
        {
            Weapon = weapon;
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
