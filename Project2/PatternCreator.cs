using System.Collections.Generic;
using UnityEngine;


namespace WayOfLove
{
    public class PatternCreator
    {
        private readonly LevelController _levelController;

        public PatternCreator(LevelController levelController)
        {
            _levelController = levelController;
            _levelController.OnLevelStart += OnLevelStart;
            _levelController.OnLevelRestart += OnLevelStart;
        }
        
        private void OnLevelStart(int level)
        {
            Shambles(_levelController.currentLevel.tiles);
        }

        private void Shambles(IEnumerable<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.Shambles(Random.Range(1, 4));
            }
        }

    }
}