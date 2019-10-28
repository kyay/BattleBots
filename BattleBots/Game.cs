using BattleBots.Properties;
using System;
using System.Media;
using System.Threading;
using System.Timers;

namespace BattleBots
{
	public class Game
	{
		public const string WEAPON_CIRCULAR_SAW = "Pikachu";
		public const string WEAPON_CLAW_CUTTER = "Squirtle";
		public const string WEAPON_FLAME_THROWER = "Pidgey";
		public const string WEAPON_SLEDGE_HAMMER = "Geodude";
		public const string WEAPON_SPINNNING_BLADE = "Swadloon";

		public static string[] WEAPONS = new string[] { WEAPON_CIRCULAR_SAW, WEAPON_CLAW_CUTTER, WEAPON_FLAME_THROWER, WEAPON_SLEDGE_HAMMER, WEAPON_SPINNNING_BLADE };
        private static ConsoleKey[] KONAMI_CODE = new ConsoleKey[] { ConsoleKey.UpArrow, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.B, ConsoleKey.A };
        private System.Timers.Timer timer;
		private Random rGen = new Random();
		private int intTimeSinceGameStart;
		private int intBattleStartTime;
		private int intTimeElapsed;

        private SoundPlayer openingSound = new SoundPlayer(Resources.Pokemon_Open);
        private SoundPlayer battleSound = new SoundPlayer(Resources.Pokemon_Battle);

        private bool blnIsBattleSoundPlaying = false;

        public Game()
		{
			timer = new System.Timers.Timer();
			timer.Enabled = true;
			timer.Interval = 1000;
			timer.Elapsed += Timer_Elapsed;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			intTimeSinceGameStart++;
			intTimeElapsed = intTimeSinceGameStart - intBattleStartTime;
		}

		public BattleBot PromptUserForBot()
		{
            openingSound.Play();
			Console.WriteLine("Do you want to enable the reading out of all the text?");
			if (Console.ReadLine().Trim().ToLower()[0] != 'y')
			{
				SpeakingConsole.EnableSpeaking = false;
			}
			SpeakingConsole.WriteLine("Welcome to Battle Bots! This is a game where there is no winning (just like life). Your goal is to get the most possible points.\n\nTo start, please enter the name for your bot:");
			string strName = SpeakingConsole.ReadLine();
			SpeakingConsole.WriteLine("\nPlease choose a weapon from the following:");

			foreach (string weapon in WEAPONS)
			{
				string[] beatableWeapons = Array.FindAll(WEAPONS, w => CanBeat(weapon, w));
				SpeakingConsole.WriteLine("\n" + weapon + " Beats " + String.Join(" And ", beatableWeapons));
			}

			string strWeapon;
			while (((strWeapon = SpeakingConsole.ReadLine()) == "" || !IsValidWeapon(strWeapon)) && strName != "")
			{
				SpeakingConsole.WriteLine("Please enter a valid weapon from above");
			}
            openingSound.Stop();
			timer.Start();
			intTimeSinceGameStart = 0;
			if (IsValidWeapon(strWeapon))
			{
				if (strName != "")
				{
					return new BattleBot(strName, GetValidWeaponName(strWeapon));
				}
				else
				{
					return new BattleBot(GetValidWeaponName(strWeapon));
				}
			}
			else
			{
				return new BattleBot();
			}
		}

		public void Battle(ref BattleBot battleBot)
        {
            if(!blnIsBattleSoundPlaying)
            {
                battleSound.PlayLooping();
                blnIsBattleSoundPlaying = true;
            }

            if (battleBot.FuelLevel > 0 && battleBot.ConditionLevel > 0)
			{
				intBattleStartTime = intTimeSinceGameStart;
				string computerWeapon = WEAPONS[rGen.Next(WEAPONS.Length)];
				SpeakingConsole.WriteLine("\nYou are being attacked by a " + computerWeapon + ". What do you do?");
				bool blnValidAction = false;
                char charReadKey = '\0';
				while (!blnValidAction)
				{
                    bool blnCheatCodeWorked = false;
					SpeakingConsole.WriteLine("\nAttack, Defend, or Retreat");
                    for(int i = 0; i < KONAMI_CODE.Length; i++)
                    {
                        ConsoleKeyInfo key = Console.ReadKey();
                        charReadKey = key.KeyChar;
                        if (key.Key != KONAMI_CODE[i])
                        {
                            break;
                        }
                        if(i == KONAMI_CODE.Length - 1)
                        {
                            battleBot.GainPoints(20);
                            SpeakingConsole.WriteLine("\nYou have cheated, trainer!! But you will get 20 extra points because that's just how the world is (unfair)");
                            blnCheatCodeWorked = true;
                        }
                    }
                    if (blnCheatCodeWorked)
                        continue;

					string strAction = SpeakingConsole.SpeakAndReturn(charReadKey + Console.ReadLine());
					switch (strAction.Trim().ToLower())
					{
						case "attack":
							blnValidAction = true;
							if(CanBeat(battleBot.Weapon, computerWeapon))
							{
								battleBot.GainPoints(5);
                                SpeakingConsole.WriteLine("You have destroyed your opponent!!");
							}
							else
							{
								battleBot.HandleDamage(5);
                                SpeakingConsole.WriteLine("You have tragically lost!!");
                            }
							battleBot.ConsumeFuel(2 * intTimeElapsed);
							break;
						case "defend":
							blnValidAction = true;
							if (CanBeat(battleBot.Weapon, computerWeapon))
							{
								battleBot.GainPoints(2);
                                SpeakingConsole.WriteLine("You have defended yourself like a noble man!!");
                            }
							else
							{
								battleBot.HandleDamage(2);
                                SpeakingConsole.WriteLine("Whoops, your shield has failed!!");
                            }
							battleBot.ConsumeFuel(intTimeElapsed);
							break;
						case "retreat":
							blnValidAction = true;
							if(rGen.Next(0, 4) == 0)
							{
								SpeakingConsole.WriteLine("Unfortunately, you couldn't escape in time!!");
								battleBot.HandleDamage(7);
                            }
                            else
                            {
                                SpeakingConsole.WriteLine("You have succesfully escaped from the battle like a coward!! No points for you!!");
                            }
							battleBot.ConsumeFuel(3 * intTimeElapsed);
							break;
						case "absorb":
							if (battleBot.Weapon == computerWeapon)
							{
								blnValidAction = true;
								SpeakingConsole.WriteLine("You have succesfully absorbed the opponent's power!!");
								battleBot.Refuel(10);
								battleBot.Heal(10);
							}
							break;
					}
					
				}
                Thread.Sleep(1000);
				SpeakingConsole.WriteLine("\nBot stats:\nName: " + battleBot.Name + ",\nWeapon: " + battleBot.Weapon + ",\nCondition Level: " + battleBot.ConditionLevel + ",\nFuel Level: " + battleBot.FuelLevel + ",\nTurn Time: " + intTimeElapsed + ",\nTotal Battle Time: " + intTimeSinceGameStart + ",\nPoints: " + battleBot.Score + ",\nHighest Score: " + battleBot.HighScore);
                Thread.Sleep(1000);
                Battle(ref battleBot);
			}
			else
			{
                battleBot.UpdateHighScore(battleBot.Score);
				SpeakingConsole.WriteLine("Your bot has lost. Do you want to play again?");
				if (SpeakingConsole.ReadLine().Trim().ToLower()[0] == 'y')
				{
					battleBot = PromptUserForBot();
					Battle(ref battleBot);
				}
			}
		}

		private static bool CanBeat(string weapon, string otherWeapon)
		{
			switch (weapon)
			{
				case WEAPON_CIRCULAR_SAW:
					if (otherWeapon == WEAPON_CLAW_CUTTER || otherWeapon == WEAPON_FLAME_THROWER)
						return true;
					break;
				case WEAPON_SLEDGE_HAMMER:
					if (otherWeapon == WEAPON_SPINNNING_BLADE || otherWeapon == WEAPON_CIRCULAR_SAW)
						return true;
					break;
				case WEAPON_SPINNNING_BLADE:
					if (otherWeapon == WEAPON_CIRCULAR_SAW || otherWeapon == WEAPON_FLAME_THROWER)
						return true;
					break;
				case WEAPON_CLAW_CUTTER:
					if (otherWeapon == WEAPON_SLEDGE_HAMMER || otherWeapon == WEAPON_SPINNNING_BLADE)
						return true;
					break;
				case WEAPON_FLAME_THROWER:
					if (otherWeapon == WEAPON_SLEDGE_HAMMER || otherWeapon == WEAPON_CLAW_CUTTER)
						return true;
					break;
			}
			return false;
		}

		private static bool IsValidWeapon(string weapon)
		{
			return Array.FindIndex(WEAPONS, s => weapon.Trim().ToLower() == s.Trim().ToLower()) != -1;
		}

		private static string GetValidWeaponName(string weapon)
		{
			return Array.Find(WEAPONS, s => weapon.Trim().ToLower() == s.Trim().ToLower());
		}
	}
}
