using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Manager
{
    public static class ManagerHelper
    {
        public static Repository.Game ToGame(this Game game)
        {
            Repository.Game repoGame = new Repository.Game();
            BowlingGame bGame;

            if (game is BowlingGame)
            {
                bGame = (BowlingGame)game;
                repoGame = bGame.ToBowlingGame();
                if (repoGame == null)
                    return null;
                repoGame.GameId = Guid.NewGuid().ToString();
                repoGame.GameType = Constants.BOWLINGBALL;
            }
            else
            {
                throw new Exception("Game not found.");
            }

            return repoGame;
        }

        #region Bowling

        public static Repository.BowlingGame ToBowlingGame(this BowlingGame bowlingGame)
        {
            if (bowlingGame == null)
                return null;

            Repository.BowlingGame bowling = new Repository.BowlingGame()
            {
                BowlingFrames = bowlingGame.BowlingFrames.ToFrame(),
                GameCategory = "InDoor",
            };

            return bowling;
        }

        public static List<Repository.Frame> ToFrame(this List<Frame> frames)
        {
            if (frames == null)
                return null;
            List<Repository.Frame> repoFrames = frames.Select(Item => new Repository.Frame(Item.FirstBowl)
            {
                SecondBowl = Item.SecondBowl,
            }).ToList();

            return repoFrames;
        }

        #endregion

        #region Manager Response

        public static GameResult ToGameResult(this Repository.GameResult gameResult)
        {
            return new GameResult()
            {
                Score = gameResult.Score
            };
        }

        #endregion
    }
}
