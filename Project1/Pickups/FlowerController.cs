using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Patterns;
using RunawayBride.UI;
#if UNITY_EDITOR
using RunawayBride.Level;
#endif



namespace RunawayBride
{
    namespace Pickups
    {
        public class FlowerController : Singleton<FlowerController>
        {

            public int FlowersAmount
            {
                get { return _flowersAmount; }
            }

            private int _flowersAmount;
            private PortalData currentPortalInfo;

            public UnityAction<int, Material> onFlowerPickup;


            public override void Awake()
            {
                base.Awake();
                onFlowerPickup += FlowerPickupHandler;
                GameStateHandler.Instance.OnPortalEnter += PortalEnterHandler;
                GameStateHandler.Instance.onLevelStart += LevelStartHandler;
                GameStateHandler.Instance.onLevelRestart += LevelStartHandler;
            }

            private void LevelStartHandler(int level)
            {
                _flowersAmount = 0;
            }

            private void FlowerPickupHandler(int amount, Material plankMaterial)
            {
                if (currentPortalInfo != null)
                {
                    if (plankMaterial == currentPortalInfo.flowerMaterial)
                    {
                        _flowersAmount += amount;
                        Character.Character.Instance.AddFlowerToHair(currentPortalInfo.flowerMaterial);
                    }
                    else
                    if (_flowersAmount >= amount)
                    {
                        _flowersAmount -= amount;
                        Character.Character.Instance.RemoveFlower();
                    }
                    else
                    {
                        GameStateHandler.Instance.onPlayerFail?.Invoke(FailReason.flower);
                    }
                }
            }

            private void PortalEnterHandler(PortalData newInfo)
            {
                currentPortalInfo = newInfo;
            }

            public bool TryToBuildFlower(int amount)
            {
                if (_flowersAmount > amount)
                {
                    _flowersAmount -= amount;
                    Character.Character.Instance.RemoveFlower();
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}