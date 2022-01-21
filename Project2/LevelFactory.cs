using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WayOfLove
{
    public class LevelFactory : ILevelFactory
    {
        private const string LevelsDataName = "MainLevelsData";
        
        private readonly DiContainer _diContainer;
        private LevelsData _levelsData;

        public int LevelsCount => _levelsData.levels.Count;

        public LevelFactory(DiContainer container)
        {
            _diContainer = container;
        }

        public Level Create(int index)
        {
            var level = _diContainer.InstantiatePrefab(_levelsData.levels[index]).GetComponent<Level>();
            return level;
        }

        public void Load()
        {
            _levelsData = Resources.Load<LevelsData>(LevelsDataName);
        }
    }
}