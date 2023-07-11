using System;
using System.Collections.Generic;

namespace Game.Repository
{
    public class BowlingRepository : IGameRepository
    {
        #region Public Methods  

        /// <summary>
        /// Method to calculate the score.
        /// </summary>
        /// <returns>Returns the calculated score.</returns>
        public GameResult CalculateScore(Game game)
        {
            GameResult gameResult = new GameResult();
            int score = 0;
            try
            {
                if (!(game is BowlingGame))
                {
                    throw new Exception("Invalid Game.");
                }

                BowlingGame bowlingGame = (BowlingGame)game;
                if (bowlingGame.BowlingFrames != null && bowlingGame.BowlingFrames.Count > 0)
                {
                    for (var index = 0; index < 10; index++)
                    {
                        Frame frame = bowlingGame.BowlingFrames[index];
                        if (frame.IsStrike)
                        {
                            // Add strike bonus.
                            score += 10 + GetStrikeBonus(bowlingGame.BowlingFrames, index);
                        }
                        else if (frame.IsSpare)
                        {
                            // Add spare bonus.
                            score += 10 + bowlingGame.BowlingFrames[index + 1].FirstBowl;
                        }
                        else
                            score += frame.Score;
                    }

                    gameResult.Score = score;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return gameResult;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to get strike bonus.
        /// </summary>
        /// <param name="index">The roll number.</param>
        /// <returns>The strike bonus.</returns>
        private int GetStrikeBonus(IList<Frame> rolls, int index)
        {
            int bonus;
            try
            {
                // The bonus for strike is the value of the next two balls rolled.
                // Get first bowl
                bonus = rolls[index + 1].FirstBowl;

                // Check if the next bowl is strike.
                if (bonus == 10)
                {
                    // If next roll is strike.
                    bonus += rolls[index + 2].FirstBowl;
                }
                else
                {
                    // If next frame is spare.
                    bonus += rolls[index + 1].SecondBowl;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonus;
        }

        #endregion
    }
}
