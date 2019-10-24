using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    public class Player
    {
        private int score;
        private int highScore;
        private string name;

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
        public int HighScore
        {
            get
            {
                return highScore;
            }
            set
            {
                highScore = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Player()
        {
            Name = "Joe";
        }

        public Player(string name)
        {
            Name = name;
        }

        public void UpdateHighScore(int newScore)
        {
            HighScore = newScore > HighScore ? newScore : HighScore;
        }

        public void GainPoints(int points)
        {
            score += points;
        }
    }
}
