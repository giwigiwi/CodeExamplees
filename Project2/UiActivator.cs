using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace WayOfLove
{
    public class UiActivator : MonoBehaviour
    {
        public GameObject completeMenu;
        public GameObject failMenu;
        public GameObject finalMenu;
        public Text levelNumberText;
        private LevelController _levelController;
        private PatternInspector _inspector;

        [Inject]
        private void Construct(LevelController levelController, PatternInspector inspector)
        {
            _levelController = levelController;
            _inspector = inspector;
        }

        private void Awake()
        {
            _levelController.OnLevelStart += OnLevelStart;
            _inspector.OnLevelComplete += OnLevelComplete;
            // _eventsHandlerService.LevelFail += OnLevelFail;
        }

        private void OnLevelStart(int number)
        {
            finalMenu.SetActive(false);
            completeMenu.SetActive(false);
            failMenu.SetActive(false);
            levelNumberText.text = (number + 1).ToString();
        }

        private void OnLevelComplete()
        {
            finalMenu.SetActive(true);
            completeMenu.SetActive(true);
        }

        // private void OnLevelFail()
        // {
        //     finalMenu.SetActive(true);
        //     failMenu.SetActive(true);
        // }
    }
}