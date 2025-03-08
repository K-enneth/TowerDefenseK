using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class Spawner : MonoBehaviour
    {
        public Transform spawnPoint;
        public List<PoolingSystem> poolingSystems;
        public List<float> poolProbabilities;
        public List<WaveConfig> waves;
        
        public int groupInterval;
        private int _waveNumber;

        public delegate void OnLastWave();
        public static event OnLastWave LastWave;

        private void Start()
        {
            StartCoroutine(SpawnWave());
        }

        private IEnumerator SpawnWave()
        {
            yield return new WaitForSeconds(groupInterval);
            while (_waveNumber < waves.Count)
            {
                WaveConfig currentWave = waves[_waveNumber];
                _waveNumber++;
                yield return StartCoroutine(SpawnGroup(currentWave));
                yield return new WaitForSeconds(groupInterval); 
            }
            
        
        }
    
        IEnumerator SpawnGroup(WaveConfig wave)
        {
           
            for (int i = 0; i < wave.enemiesInGroup; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(wave.enemySpawnDelay); 
            }
            if (_waveNumber >= waves.Count)
            {
                Debug.Log("LastWave");
                LastWave?.Invoke();
            }
            
        }

        private void SpawnEnemy()
        {
            PoolingSystem selectedPool = ChoosePool();
            selectedPool.AskForObject(spawnPoint);
            
        }

        private PoolingSystem ChoosePool()
        {
            float weight = 0;
            for (int indexProbability = 0; indexProbability < poolProbabilities.Count; indexProbability++)
            {
                weight += poolProbabilities[indexProbability];
            }
            
            float randomValue = Random.Range(0, weight);
            float totalWeight = 0;

            for (int indexPool = 0; indexPool < poolingSystems.Count; indexPool++)
            {
                totalWeight += poolProbabilities[indexPool];
                if (randomValue < totalWeight)
                {
                    return poolingSystems[indexPool];
                }
            }

            return poolingSystems[0];
        }
        
        [Serializable]
        public struct WaveConfig
        {
            public float enemySpawnDelay;
            public int enemiesInGroup;
        }
    }
}
