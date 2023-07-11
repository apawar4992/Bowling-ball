namespace Game.Manager
{
    /// <summary>
    /// Represents the Frame of the bowling game.
    /// </summary>
    public class Frame
    {
        #region Constructor

        /// <summary>
        /// Represents frame constructor.
        /// </summary>
        /// <param name="firstBowl"></param>
        public Frame(int firstBowl)
        {
            FirstBowl = firstBowl;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The first bowl.
        /// </summary>
        private int firstBowl = 0;

        /// <summary>
        /// The second bowl.
        /// </summary>
        private int secondBowl = 0;

        #endregion

        #region Public Members

        /// <summary>
        /// The first bowl.
        /// </summary>
        public int FirstBowl
        {
            get => firstBowl;
            set => firstBowl = value;
        }

        /// <summary>
        /// The second bowl.
        /// </summary>
        public int SecondBowl
        {
            get => secondBowl;
            set => secondBowl = value;
        }

        #endregion
    }
}
