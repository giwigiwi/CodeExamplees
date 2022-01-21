using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Patterns;
using RunawayBride.Bridge;

#if UNITY_EDITOR
using UnityEditor;
#endif



namespace RunawayBride
{
    namespace Level
    {
        [System.Serializable]
        public struct ElementBox
        {
            public int bridgeLenght;
            public int bridgePosition;
            public List<LevelElement> elements;
        }

        public class LevelBuilder : Singleton<LevelBuilder>
        {

            public int loopIndexStart = 0;
            public List<ElementBox> levels = new List<ElementBox>();
            [HideInInspector] public int lastLevelIndex;
            public ElementBox CurrentLevel => _currentLevel;

            private List<LevelElement> _currrentElements = new List<LevelElement>();
            private ElementBox _currentLevel;


            public override void Awake()
            {
                base.Awake();
            }

            private void Start()
            {
                lastLevelIndex = ES3.Load<int>("Last Level Index", 0);
            }
            
            public List<LevelElement> BuildLevel(List<LevelElement> elements)
            {
                List<LevelElement> buildedLevel = new List<LevelElement>();
                for (int i = 0; i < elements.Count; i++)
                {
                    LevelElement createdElement = Instantiate(elements[i], Vector3.zero, Quaternion.identity);
                    createdElement.transform.SetParent(LevelController.Instance.level.transform);
                    if (i == _currentLevel.bridgePosition)
                        BridgeSpawner.Instance.SpawnBridgeElements(createdElement);
                    buildedLevel.Add(createdElement);
                }
                ArrangeLevel(buildedLevel);
                return buildedLevel;
            }

            public List<LevelElement> CreateNewLevel(int levelNumber)
            {
                if (levelNumber < levels.Count)
                {
                    lastLevelIndex = levelNumber;
                    _currentLevel = levels[levelNumber];
                }
                else
                {
                    lastLevelIndex = Random.Range(loopIndexStart, levels.Count);
                    _currentLevel = levels[lastLevelIndex];
                }
                _currrentElements = _currentLevel.elements;
                ES3.Save("Last Level Index", lastLevelIndex);
                return BuildLevel(_currrentElements);
            }


            private void ArrangeLevel(List<LevelElement> elements)
            {
                LevelElement previousElement = null;
                for (int i = 0; i < elements.Count; i++)
                {
                    if (previousElement != null)
                        elements[i].transform.position = previousElement.transform.position + previousElement.exit - elements[i].enter;
                    else
                        elements[i].transform.position = Vector3.zero - elements[i].enter;
                    previousElement = elements[i];
                }
            }
        }
    }
}