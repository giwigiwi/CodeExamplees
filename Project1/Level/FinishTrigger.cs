using UnityEngine;



namespace RunawayBride
{
    namespace Level
    {
        public class FinishTrigger : MonoBehaviour
        {


            private void OnTriggerEnter(Collider other)
            {
                FinishLevel();
            }

            private void FinishLevel()
            {
                GameStateHandler.Instance.onPlayerFinish?.Invoke();
            }
        }
    }
}
