using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RunawayBride
{
    namespace Level
    {
        public class LevelElement : MonoBehaviour
        {
            public Vector3 enter;
            public Vector3 exit;


            private void OnDrawGizmos()
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position + enter, 0.5f);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position + exit, 0.5f);
            }

        }
    }

}