using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using RunawayBride.Level;
using System.Linq;



namespace RunawayBride
{
    namespace Bridge
    {
        public class BridgeSpawner : Singleton<BridgeSpawner>
        {
            public float distanceBetweenParts;
            public float partsHeight;
            public BridgeData data;
            [HideInInspector]
            public List<BridgePart> bridgeParts;

            public override void Awake()
            {
                base.Awake();
                
                bridgeParts = new List<BridgePart>();
            }

            public List<BridgePart> SpawnBridgeElements(LevelElement bridgeElement)
            {
                ElementBox currentLevel = LevelBuilder.Instance.CurrentLevel;
                data.bridgeLenght = currentLevel.bridgeLenght;
                bridgeParts.Clear();
                BridgePart part;
                for (int i = 0; i < data.bridgeLenght; i++)
                {
                    part = Instantiate(data.GetColoredPlank(data.blankMaterial),
                                        new Vector3(0, partsHeight, i * (data.partPrefab.transform.localScale.z + distanceBetweenParts)),
                                        Quaternion.identity,
                                        bridgeElement.gameObject.transform);
                    CorrectPartCollider(part, i);
                    bridgeParts.Add(part);
                }
                SetEnterAndExitOfBridge(bridgeElement);
                return bridgeParts;
            }

            private void SetEnterAndExitOfBridge(LevelElement bridge)
            {
                bridge.enter = new Vector3(0, 0, bridgeParts[0].transform.localPosition.z - data.partPrefab.transform.localScale.z / 2 - distanceBetweenParts);
                bridge.exit = new Vector3(0, 0, bridgeParts.Last().transform.localPosition.z + data.partPrefab.transform.localScale.z / 2 + distanceBetweenParts);
            }

            private void CorrectPartCollider(BridgePart part, int index)
            {
                Collider col = part.coll;
                if (index == 0 || index == data.bridgeLenght - 1)
                {
                    col.transform.localScale = new Vector3(col.transform.localScale.x, col.transform.localScale.y,
                    ((distanceBetweenParts * 1.5f) / part.transform.localScale.z) + part.transform.localScale.z);
                    col.transform.localPosition = new Vector3(0, 0, Mathf.Sign(index - 1) * (distanceBetweenParts / 4));
                }
                else
                    col.transform.localScale = new Vector3(col.transform.localScale.x, col.transform.localScale.y,
                    (distanceBetweenParts / part.transform.localScale.z) + part.transform.localScale.z);
            }
        }
    }
}