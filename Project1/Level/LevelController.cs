using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Patterns;



namespace RunawayBride
{
    namespace Level
    {
        public class LevelController : Singleton<LevelController>
        {
            public GameObject level;
            public int currentLevelNumber;


#if UNITY_EDITOR
            public bool isLevelTesting;
            public int levelToLoad;
#endif

            [HideInInspector] public List<LevelElement> currentLevel = new List<LevelElement>();


            public override void Awake()
            {
                base.Awake();
#if UNITY_EDITOR
                if (isLevelTesting)
                {
                    currentLevelNumber = levelToLoad;
                    return;
                }
#endif
                currentLevelNumber = ES3.Load<int>("Current Level Number", 0);
            }

            public void StartNextLevel()
            {
                GameStateHandler.Instance.onLevelStart?.Invoke(currentLevelNumber);
            }

            public void RestartLevel()
            {
                GameStateHandler.Instance.onLevelRestart?.Invoke(currentLevelNumber);
            }

            private void Start()
            {
                GameStateHandler.Instance.onLevelStart += LoadLevel;
                GameStateHandler.Instance.onLevelRestart += RestartLevel;
                GameStateHandler.Instance.onPlayerFinish += IncreaseLevelNumber;
                GameStateHandler.Instance.onLevelRestart?.Invoke(currentLevelNumber);
            }


            private void IncreaseLevelNumber()
            {
#if UNITY_EDITOR
                if (isLevelTesting)
                {
                    return;
                }
#endif
                currentLevelNumber += 1;
                ES3.Save("Current Level Number", currentLevelNumber);
            }

            private void LoadLevel(int levelNumber)
            {
                if (currentLevel.Count > 0)
                    ClearLevel(currentLevel);
                currentLevel = LevelBuilder.Instance.CreateNewLevel(levelNumber);
            }

            private void RestartLevel(int levelNumber)
            {
                LoadLevel(SelectLastLevel());
            }

            private void ClearLevel(List<LevelElement> elements)
            {
                foreach (LevelElement element in elements)
                    Destroy(element.gameObject);
                elements.Clear();
            }

            private int SelectLastLevel()
            {
                if (currentLevelNumber < LevelBuilder.Instance.levels.Count)
                    return currentLevelNumber;
                else
                    return ES3.Load<int>("Last Level Index", 0);
            }

        }

    }

}