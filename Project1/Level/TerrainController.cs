using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunawayBride.Input;




namespace RunawayBride
{
    namespace Level
    {
        public class TerrainController : MonoBehaviour
        {

            public float defaultSpeed;
            public float widthStart;
            public float widthEnd;
            public float finishPosition;
            public int environmentPointCount;
            public List<GameObject> environments = new List<GameObject>();
            public GameObject environmentsContainer;
            
            private List<GameObject> _environmentPoints = new List<GameObject>();

            private float _speed;
            private List<float> _xValues = new List<float>();
            private List<float> _zValues = new List<float>();


            private void Start()
            {
                GameStateHandler.Instance.onPlayerFinish += FinishLevelHandler;
                GameStateHandler.Instance.onPlayerFail += FailHandler;
                GameStateHandler.Instance.onLevelStart += StartLevelHandler;
                GameStateHandler.Instance.onLevelRestart += StartLevelHandler;
                PlayerInput.Instance.OnInputProcess += HandlePlayerInput;
            }

            private void FixedUpdate()
            {
                if (_speed != 0)
                {
                    transform.position += (Vector3.back * defaultSpeed * Time.fixedDeltaTime);
                    environmentsContainer.transform.position += (Vector3.back * defaultSpeed * Time.fixedDeltaTime);
                }
            }

            private void FinishLevelHandler()
            {
                _speed = 0;
            }

            private void FailHandler(FailReason reason)
            {
                _speed = 0;
            }

            private void StartLevelHandler(int LevelNumber)
            {
                transform.position = new Vector3(0, transform.position.y, 0);
                ClearSurround();
                GenerateSurround();
            }

            private void ClearSurround()
            {
                foreach (GameObject element in _environmentPoints)
                    Destroy(element.gameObject);
                _environmentPoints.Clear();

            }

            private void GenerateSurround()
            {
                GameObject env;
                if (environments.Count > 0)
                    for (int i = 0; i < environmentPointCount; i++)
                    {
                        env = Instantiate(environments[Random.Range(0, environments.Count)], ChooseRandomPosition(), Quaternion.Euler(0, Random.Range(0, 181), 0));
                        env.transform.SetParent(environmentsContainer.transform);
                        _environmentPoints.Add(env);
                    }
            }

            private Vector3 ChooseRandomPosition()
            {
                int side;
                if (Random.Range(0, 2) == 0)
                    side = -1;
                else
                    side = 1;
                float x = Random.Range(widthStart, widthEnd) * side;
                float z = Random.Range(0f, finishPosition);

                return new Vector3(x, transform.position.y, z);
            }


            private float GetUniquePoint(List<float> axisValues, float start, float end)
            {
                float point = Random.Range(start, end);
                if (CheckPoint(axisValues, point))
                    return 1f;
                else
                    return GetUniquePoint(axisValues, start, end);
            }

            private bool CheckPoint(List<float> axisValues, float point)
            {
                foreach (int value in axisValues)
                    if (Mathf.Abs(value - point) < 5)
                        break;
                    else
                        return true;
                return false;
            }

            private void HandlePlayerInput(Vector2 screenDelta)
            {
                _speed = defaultSpeed;
            }

        }
    }
}

