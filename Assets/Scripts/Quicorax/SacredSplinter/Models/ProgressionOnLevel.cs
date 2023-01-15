using System;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class ProgressionOnLevel
    {
        public ProgressionOnLevel(string levelName, int maxLevel, bool completed)
        {
            LevelName = levelName;
            MaxLevel = maxLevel;
            Completed = completed;
        }

        public string LevelName;
        public int MaxLevel;
        public bool Completed;
    }
}