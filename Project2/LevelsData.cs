using System.Collections.Generic;
using UnityEngine;

namespace WayOfLove
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "New Levels data", order = 0)]
    public class LevelsData : ScriptableObject
    {
        public List<Level> levels;
    }
}