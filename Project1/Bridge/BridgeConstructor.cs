using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunawayBride.Pickups;
using Patterns;
using UnityEngine.Events;



namespace RunawayBride
{
    namespace Bridge
    {
        public class BridgeConstructor : Singleton<BridgeConstructor>
        {
            public Material blankFlowerMaterial;

            public PortalData ActualColor => _actualColor;

            private BridgeSpawner _spawner;
            private PortalData _actualColor;

            public UnityAction<int, BridgePart> onPartStep;
            public UnityAction<bool, BridgePart> onPartConstruct;

            private bool _isFirstStep = true;

            override public void Awake()
            {
                base.Awake();

                _spawner = BridgeSpawner.Instance;
                GameStateHandler.Instance.OnPortalEnter += PortalEnterHandler;
                GameStateHandler.Instance.onLevelStart += StartLevelHandler;
                GameStateHandler.Instance.onLevelRestart += StartLevelHandler;
                onPartStep += PartStepHandler;
            }

            private void PartStepHandler(int amount, BridgePart origin)
            {
                if (_isFirstStep)
                {
                    _isFirstStep = false;
                    foreach (BridgePart part in _spawner.bridgeParts)
                    {
                        part.ChangeMaterial(blankFlowerMaterial);
                        part.ShowChadow(false);
                    }
                }
                onPartConstruct?.Invoke(FlowerController.Instance.TryToBuildFlower(amount), origin);
            }

            private void PortalEnterHandler(PortalData data)
            {
                _actualColor = data;
            }

            private void StartLevelHandler(int level)
            {
                _isFirstStep = true;
            }

        }
    }
}