using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    public sealed class BattleBot : Vehicle
    {
        public override void Heal(int amount)
        {
            ConditionLevel += amount;
        }

        public override void Refuel(int amount)
        {
            FuelLevel += amount;
        }

        public BattleBot()
        {
            Name = "Mr. Letts";
            Weapon = Game.WEAPON_FLAME_THROWER;
        }

        public BattleBot(string weapon)
        {
            Name = "Mr. Letts";
            Weapon = weapon;
        }
        public BattleBot(string name, string weapon)
        {
            Name = name;
            Weapon = weapon;
        }

    }
}
