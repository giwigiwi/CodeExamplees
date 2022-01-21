using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunawayBride.Bridge;
using System.Linq;



namespace RunawayBride
{
    [CreateAssetMenu(fileName = "new BridgeData", menuName = "Scriptable Objects/Bridge Data", order = 2)]
    public class BridgeData : ScriptableObject
    {
        public int bridgeLenght;
        public Material blankMaterial;
        public List<Material> colorMaterials;
        public BridgePart partPrefab;

        public BridgePart GetRandomColorPlank(List<Material> colors)
        {
            BridgePart part = partPrefab;
            part.GetComponent<MeshRenderer>().sharedMaterial = colors[Random.Range(0, colors.Count)];
            return part;
        }

        public BridgePart GetRandomColorPlank(Material[] colors)
        {
            BridgePart part = partPrefab;
            part.GetComponent<MeshRenderer>().sharedMaterial = colors[Random.Range(0, colors.Length)];
            return part;
        }

        public BridgePart GetColoredPlank(Material color)
        {
            BridgePart part = partPrefab;
            part.GetComponent<MeshRenderer>().sharedMaterial = color;
            return part;
        }

        public Material GetRandomColor(IEnumerable<Material> colors)
        {
            return colors.ToList<Material>()[Random.Range(0,colors.Count<Material>())];
        }

        public Material GetRandomColor(Material[] colors)
        {
            return colors[Random.Range(0, colors.Length)];
        }

    }
}
