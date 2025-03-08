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
        [SerializeField] private List<Enemy> _enemiesAlive;
        public Canvas winCanvas;
        public Canvas loseCanvas;
        private float _enemyCount;
        
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
            Debug.Log("Game Over");
        }

        private void WinGame()
        {
            winCanvas.enabled = true;
            Time.timeScale = 0;
            Debug.Log("YOU WIN");
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
        
        private void GetEnemiesAlive()
        {
            Debug.Log("GETTING ENEMIES ALIVE");
            _enemiesAlive= FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
            _enemyCount= _enemiesAlive.Count;
        }

        private void CheckEnemies()
        {
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
