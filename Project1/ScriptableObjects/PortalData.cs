using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;




namespace RunawayBride
{

    [CreateAssetMenu(fileName = "new PortalData", menuName = "Scriptable Objects/Portal Data", order = 1)]
    public class PortalData : ScriptableObject
    {

        public Material portalMaterial;
        public Material cornerMaterial;
        public Material flowerMaterial;
        public Material shirtMaterial;
        public Material feetMaterial;

    }

}