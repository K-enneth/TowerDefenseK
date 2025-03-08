using System.Collections.Generic;
using System.Linq;
using Enemies;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
            public Canvas winCanvas;
            public Canvas loseCanvas;
            private List<Enemy> _enemiesAlive;
            private float _enemyCount;
        #endregion
        
        //Manages game over and restart game
        
        public void OnEnable()
        {
            PlayerBase.OnGameOver += GameOver;
            Spawner.LastWave += GetEnemiesAlive;
            Enemy.DeadEvent += CheckEnemies;
        }

        public void OnDisable()
        {
            PlayerBase.OnGameOver -= GameOver;
            Spawner.LastWave -= GetEnemiesAlive;
            Enemy.DeadEvent += CheckEnemies;
        }
        
        private void GameOver()
        {
            loseCanvas.enabled = true;
            Time.timeScale = 0;
        }

        private void WinGame()
        {
            winCanvas.enabled = true;
            Time.timeScale = 0;
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
        
        private void GetEnemiesAlive()
        {
            //Getting enemies left from last wave
            _enemiesAlive= FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
            _enemyCount= _enemiesAlive.Count;
        }

        private void CheckEnemies()
        {
            //Only checks enemies alive when it's the last wave
            if(_enemiesAlive==null) return;
            for (int indexEnemy = 0; indexEnemy < _enemiesAlive.Count; indexEnemy++)
            {
                if (!_enemiesAlive[indexEnemy].gameObject.activeInHierarchy)
                {
                    _enemiesAlive.RemoveAt(indexEnemy);
                    _enemyCount--;
                    
                    if (_enemyCount <= 1)
                    {
                        WinGame();
                    }
                }
            }
        }

    }
}
