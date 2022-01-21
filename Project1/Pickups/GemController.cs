using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using UnityEngine.Events;
using RunawayBride.UI;



namespace RunawayBride
{
    namespace Pickups
    {
        public class GemController : Singleton<GemController>
        {
            public int GemsAmount
            {
                get { return _gemsAmount; }
            }

            private int _gemsAmount;
            public UnityAction<int> onGemPickup;


            public override void Awake()
            {
                base.Awake();
                onGemPickup += GemPickupHandler;
                _gemsAmount = ES3.Load<int>("gems", 0);
                UpdateGemUI();
            }

            private void GemPickupHandler(int amount)
            {
                _gemsAmount += amount;
                ES3.Save("gems", _gemsAmount);
                UpdateGemUI();
            }

            private void UpdateGemUI()
            {
                UIUpdater.UpdateTextInfo(UIHandler.Instance.gems, _gemsAmount.ToString());
            }
        }
    }
}