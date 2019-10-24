using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    public abstract class Vehicle
    {
        public int FuelLevel { get; set; } = 50;
        public int ConditionLevel { get; set; } = 50;

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
