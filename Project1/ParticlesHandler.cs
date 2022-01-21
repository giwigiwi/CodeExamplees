using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RunawayBride
{
    public class ParticlesHandler : MonoBehaviour
    {
        // public ParticleSystem endLevelConfetti;

        [Header("smiles")]
        public ParticleSystem endLevelSmiles;
        public List<Sprite> failSmiles;
        public List<Sprite> succesSmiles;

        [Header("Fireworks")]
        public List<ParticleSystem> endLevelFireworks;
        public GameObject container;
        public float delayBetweenBlow;
        public int FireworksAmount;

        private int fireworksSpawned = 0;
        private List<ParticleSystem> activeParticles;


        private void Awake()
        {
            activeParticles = new List<ParticleSystem>();
            GameStateHandler.Instance.onLevelRestart += StopAllParticles;
            GameStateHandler.Instance.onLevelStart += StopAllParticles;
            GameStateHandler.Instance.onPlayerFinish += PlayerFinishHandler;
            GameStateHandler.Instance.onPlayerFail += PlayerFailHandler;
        }

        private void StopAllParticles(int level)
        {
            StopAllCoroutines();
            foreach (ParticleSystem particle in activeParticles)
                StopParticle(particle);
            activeParticles.Clear();
        }

        public void ActivateParticle(ParticleSystem particle)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
            activeParticles.Add(particle);
        }

        public void StopParticle(ParticleSystem particle)
        {
            particle.Stop();
            particle.gameObject.SetActive(false);
        }

        private void PlayerFinishHandler()
        {
            // ActivateParticle(endLevelConfetti);
            StartCoroutine(StartFireworks());
            StartEndSmiles(endLevelSmiles, succesSmiles);
        }

        private void PlayerFailHandler(FailReason reason)
        {
            StartEndSmiles(endLevelSmiles, failSmiles);
        }

        private void StartEndSmiles(in ParticleSystem particle, List<Sprite> smiles)
        {
            RemoveSmiles(particle);
            AddSmiles(particle, smiles);
            ActivateParticle(particle);
        }

        private IEnumerator StartFireworks()
        {
            fireworksSpawned = 0;
            while (fireworksSpawned < FireworksAmount)
            {
                Instantiate(endLevelFireworks[Random.Range(0, endLevelFireworks.Count)],
                            container.transform.position + new Vector3(Random.Range(-5f, 1f), Random.Range(3f, 10f), Random.Range(-2f, 2f)),
                            Quaternion.identity, container.transform);
                fireworksSpawned++;
                yield return new WaitForSeconds(delayBetweenBlow);
                yield return null;
            }
        }

        private void AddSmiles(in ParticleSystem particle, List<Sprite> smiles)
        {
            for (int i = 0; i < smiles.Count; i++)
                particle.textureSheetAnimation.AddSprite(smiles[i]);
        }

        private void RemoveSmiles(in ParticleSystem particle)
        {
            while (particle.textureSheetAnimation.spriteCount > 0)
                particle.textureSheetAnimation.RemoveSprite(0);
        }

    }
}