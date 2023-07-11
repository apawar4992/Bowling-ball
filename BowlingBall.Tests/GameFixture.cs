using System;
using System.Collections.Generic;
using System.Linq;
using Game.Manager;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BowlingBall.Tests
{
    public class GameFixture
    {
        private GameManager gameManager = null;
        private BowlingGame bowlingGame = null;
        private Game.Manager.Game game = null;

        public GameFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IGameFactory, GameFactory>();

            serviceCollection.AddTransient<serviceResolver>(servieProvider => gameType =>
            {
                switch (gameType)
                {
                    case Constants.BOWLINGBALL:
                        return new Game.Repository.BowlingRepository();
                    default:
                        throw new Exception("Invalid Game");
                }
            });
            
            gameManager = new GameManager(serviceCollection.BuildServiceProvider());
            
            game = new BowlingGame();
            game.GameType = Constants.BOWLINGBALL;
            game.GameId = 1;
            bowlingGame = (BowlingGame)game;
        }


        [Theory]
        [InlineData(new int[] { 10, 9, 1, 5, 5, 7, 2, 10, 10, 10, 9, 0, 8, 2, 9, 1, 10 })]
        public void BowlingBall_ShouldReturnValidScore_ForValidInput(int[] arr)
        {
            bowlingGame.BowlingFrames = AddRolls(arr)?.ToList();
            GameResult gameResult = gameManager.GetResult(game);

            Assert.NotNull(gameResult);
            Assert.Equal(187, gameResult.Score);
        }

        [Theory]
        [InlineData(null)]
        public void BowlingBall_ShouldReturnZeroScore_WhenRollsAreNull(int[] arr)
        {
            bowlingGame.BowlingFrames = AddRolls(arr)?.ToList();
            GameResult gameResult = gameManager.GetResult(game);
            Assert.NotNull(gameResult);
            Assert.Equal(0, gameResult.Score);
        }

        [Theory]
        [InlineData(new int[] { })]
        public void BowlingBall_ShouldReturnZeroScore_WhenAllRollsAreFoul(int[] arr)
        {
            bowlingGame.BowlingFrames = AddRolls(arr)?.ToList();
            GameResult gameResult = gameManager.GetResult(game);
            Assert.NotNull(gameResult);
            Assert.Equal(0, gameResult.Score);
        }

        [Theory]
        [InlineData(new int[] { 1, 3, 7, 3, 10, 1, 7, 5, 2, 5, 3, 8, 2, 8, 2, 10, 9, 0 })]
        public void BowlingBall_ShouldReturn_ValidScoreWithNoExtraRoll(int[] arr)
        {
            bowlingGame.BowlingFrames = AddRolls(arr).ToList();
            GameResult gameResult = gameManager.GetResult(game);

            Assert.NotNull(gameResult);
            Assert.Equal(131, gameResult.Score);
        }

        [Theory]
        [InlineData(new int[] { 1, 3, 7, 3, 10, 1, 7, 5, 2, 5, 3, 8, 2, 8, 2, 10, 9, 1, 10 })]
        public void BowlingBall_ShouldReturn_ValidScoreWithSpareThenStrikeAtEnd(int[] arr)
        {
            bowlingGame.BowlingFrames = AddRolls(arr).ToList();
            GameResult gameResult = gameManager.GetResult(game);

            Assert.NotNull(gameResult);
            Assert.Equal(143, gameResult.Score);
        }

        [Theory]
        [InlineData(new int[] { 1, 3, 7, 3, 10, 1, 7, 5, 2, 5, 3, 8, 2, 8, 2, 10, 10, 10, 10 })]
        public void BowlingBall_ShouldReturn_ValidScoreWithThreeStrikesAtEnd(int[] arr)
        {
            bowlingGame.BowlingFrames = AddRolls(arr).ToList();
            GameResult gameResult = gameManager.GetResult(game);

            Assert.NotNull(gameResult);
            Assert.Equal(163, gameResult.Score);
        }


        #region Private Methods
        
        /// <summary>
        /// Method to add rolls.
        /// </summary>
        /// <param name="pins"></param>
        private List<Frame> AddRolls(int[] pins)
        {
            bool isFirstBowlSpareOrStrike = true;
            List<Frame> frames = null;
            try
            {
                if (pins != null && pins.Count() > 0)
                {
                    frames = new List<Frame>();
                    for (int index = 0; index < pins.Length; index++)
                    {
                        if (isFirstBowlSpareOrStrike)
                        {
                            // for first bowl.
                            frames.Add(new Frame(pins[index]));
                            isFirstBowlSpareOrStrike = pins[index] == 10;
                        }
                        else
                        {
                            // for second bowl.
                            var last = frames.LastOrDefault();
                            last.SecondBowl = pins[index];
                            isFirstBowlSpareOrStrike = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return frames;
        }

        #endregion
    }
}
