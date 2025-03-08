using Enemies;
using UnityEngine;

namespace Player
{
    public class PlayerBase : MonoBehaviour
    {
        public delegate void OnGameOverDelegate();
        public delegate void OnHealthChangedDelegate(int health);
        public static event OnGameOverDelegate OnGameOver;
        public static event OnHealthChangedDelegate OnHurt;
        
        public int health;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Enemy enemy)) return;
            LoseHealth(enemy.damage);
            enemy.End();
        }

        private void LoseHealth(int damage)
        {
            health -= damage;
            OnHurt?.Invoke(health);
            if (health <= 0)
            {
                OnGameOver?.Invoke();
            }
        }
        
    }
}
