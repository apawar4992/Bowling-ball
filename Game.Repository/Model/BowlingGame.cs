using System.Collections.Generic;

namespace Game.Repository
{
    public class BowlingGame : Game
    {
        public List<Frame> BowlingFrames { get; set; }

        public int FinalScore { get; set; }
    }
}
