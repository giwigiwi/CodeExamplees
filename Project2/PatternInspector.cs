using System;
using System.Linq;
using UnityEngine.Events;
using Zenject;

namespace WayOfLove
{
    public class PatternInspector
    {
        private Tile[] _currentTiles;
        private readonly LevelController _levelController;

        public event UnityAction OnLevelComplete;

        public PatternInspector(LevelController levelController)
        {
            _levelController = levelController;
            _levelController.OnLevelStart += OnLevelStart;
            _levelController.OnLevelRestart += OnLevelStart;
        }
        
        public void TileRotated()
        {
            if (_currentTiles.Any(tile => !tile.CheckRotation())) return;
            OnLevelComplete?.Invoke();
        }
        
        private void OnLevelStart(int level)
        {
            _currentTiles = _levelController.currentLevel.tiles;
        }

    }
}