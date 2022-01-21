using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace WayOfLove
{
    public partial class LevelController : MonoBehaviour
    {
        private const string CurrentLevelNumberText = "Current Level Number";
        private const string LastLevelIndexText = "Last Level Index";

        public Level currentLevel;

        [SerializeField] private int loopIndexStart = 3;

        public event UnityAction<int> OnLevelStart;
        public event UnityAction<int> OnLevelRestart;

        [Tooltip("Core value")] [SerializeField]
        private int _currentLevelNumber;

        public int CurrentLevelNumber => _currentLevelNumber;

        private int _lastLevelIndex;
        private LevelFactory _levelFactory;

#if UNITY_EDITOR
        [Header("Use for testing")] public bool isLevelTesting;
        public int levelToLoad;

#endif

        [Inject]
        private void Construct(LevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
        }

        private void Awake()
        {
            ES3.Init();
        }

        private void Start()
        {
            OnLevelRestart += RestartLevel;
#if UNITY_EDITOR
            if (isLevelTesting)
            {
                _currentLevelNumber = levelToLoad;
                LoadLevel(_currentLevelNumber);
                return;
            }
#endif
            _currentLevelNumber = ES3.Load(CurrentLevelNumberText, 0);
            SelectLastLevel();
            _lastLevelIndex = SelectLastLevel();
            LoadLevel(_lastLevelIndex);
        }

        public void StartNextLevel()
        {
            IncreaseLevelNumber();
            OnLevelStart?.Invoke(_currentLevelNumber);
        }

        public void RestartLevel()
        {
            OnLevelRestart?.Invoke(_currentLevelNumber);
        }


        private void IncreaseLevelNumber()
        {
#if UNITY_EDITOR
            if (isLevelTesting)
            {
                LoadLevel(_currentLevelNumber);
                return;
            }
#endif
            _currentLevelNumber += 1;
            ES3.Save(CurrentLevelNumberText, _currentLevelNumber);
            LoadLevel(_currentLevelNumber);
        }

        private void CreateLevel(int levelNumber)
        {
            if (levelNumber < _levelFactory.LevelsCount)
            {
                _lastLevelIndex = levelNumber;
                currentLevel = _levelFactory.Create(levelNumber);
            }
            else
            {
                 int newIndex = GetNewIndex(loopIndexStart, _levelFactory.LevelsCount);
                 while(newIndex == _lastLevelIndex)
                 {
                     newIndex = GetNewIndex(loopIndexStart, _levelFactory.LevelsCount);
                 }
                 _lastLevelIndex = newIndex;
                currentLevel = _levelFactory.Create(_lastLevelIndex);
            }
        }

        private int GetNewIndex(int start, int end)
        {
            return Random.Range(start, end);
        }

        private void LoadLevel(int levelNumber)
        {
            ClearLevel(currentLevel);
            CreateLevel(levelNumber);
            ES3.Save(LastLevelIndexText, _lastLevelIndex);
            OnLevelStart?.Invoke(_lastLevelIndex);
        }

        private void RestartLevel(int levelNumber)
        {
            LoadLevel(SelectLastLevel());
        }

        private void ClearLevel(Level level)
        {
            if (level != null) Destroy(level.gameObject);
        }

        private int SelectLastLevel()
        {
            return _currentLevelNumber < _levelFactory.LevelsCount ? _currentLevelNumber : ES3.Load(LastLevelIndexText, 0);
        }
    }
}