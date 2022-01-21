using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Patterns;
using RunawayBride.Input;



namespace RunawayBride
{
    namespace UI
    {
        [System.Serializable]
        public struct ReasonToText
        {
            public FailReason reason;
            public string text;
        }
        public class UIHandler : Singleton<UIHandler>
        {
            [Header("Texts")]
            public Text levelNumber;
            public Text gems;
            public Text FailText;
            public List<ReasonToText> failTexts;

            [Header("Menus")]
            public GameObject endLevelMenu;
            public GameObject failMenu;
            public GameObject succesMenu;
            public GameObject swipeToStart;

            private const string FailPattern = "Oops! {0}";

            public override void Awake()
            {
                base.Awake();

                GameStateHandler.Instance.onLevelStart += StartLevelHandler;
                GameStateHandler.Instance.onLevelRestart += StartLevelHandler;
                GameStateHandler.Instance.onPlayerFail += PlayerFailHandler;
                GameStateHandler.Instance.onPlayerFinish += PlayerFinishHandler;
                PlayerInput.Instance.OnInputProcess += HandlePlayerInput;
            }

            private void StartLevelHandler(int level)
            {
                UIUpdater.UpdateTextInfo(levelNumber, level + 1);
                ChangeEndLevelMenuState(false, false);
                swipeToStart.SetActive(true);
            }

            private void PlayerFailHandler(FailReason reason)
            {
                ChangeEndLevelMenuState(true, false);
                for (int i = 0; i < failTexts.Count; i++)
                    if (failTexts[i].reason == reason)
                        UIUpdater.UpdateTextInfo(FailText, string.Format(FailPattern, failTexts[i].text));
            }

            private void PlayerFinishHandler()
            {
                ChangeEndLevelMenuState(true, true);
            }

            private void ChangeEndLevelMenuState(bool isShow, bool isSucces)
            {
                if (isShow)
                {
                    endLevelMenu.SetActive(isShow);
                    if (isSucces)
                    {
                        succesMenu.SetActive(isSucces);
                        failMenu.SetActive(!isSucces);
                    }
                    else
                    {
                        succesMenu.SetActive(isSucces);
                        failMenu.SetActive(!isSucces);
                    }
                }
                else
                {
                    endLevelMenu.SetActive(isShow);
                    succesMenu.SetActive(isShow);
                    failMenu.SetActive(isShow);
                }
            }

            private void HandlePlayerInput(Vector2 screenDelta)
            {
                swipeToStart.SetActive(false);
            }

        }
    }
}
