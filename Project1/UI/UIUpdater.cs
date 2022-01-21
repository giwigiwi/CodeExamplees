using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




namespace RunawayBride
{
    namespace UI
    {
        public static class UIUpdater
        {
            public static void UpdateTextInfo(Text element, string text)
            {
                element.text = text;
            }

            public static void UpdateTextInfo(Text element, int text)
            {
                element.text = text.ToString();
            }

        }
    }
}