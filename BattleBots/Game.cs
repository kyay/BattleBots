using System.Windows.Forms;

namespace BattleBots
{
    public class Game
    {
        private Timer timer;
        private int intTimeSinceGameStart;
        private int intBattleStartTime;
        private int intTimeElapsed;

        public Game()
        {
            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            intTimeSinceGameStart++;
            intTimeElapsed = intTimeSinceGameStart - intBattleStartTime;
            }

        public BattleBot PromptUserForBot()
        {
            return null;
        }
    }
}
