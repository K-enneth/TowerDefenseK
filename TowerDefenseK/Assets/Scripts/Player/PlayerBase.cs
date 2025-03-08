using Enemies;
using UnityEngine;

namespace Player
{
    public class PlayerBase : MonoBehaviour
    {
        #region Delegates and Events
            //Creo q lo pude haber acomodado para q solo fuera un delegate pero ya me dio flojera la vdd aaaa
            public delegate void OnGameOverDelegate();
            public delegate void OnHealthChangedDelegate(int health);
            public static event OnGameOverDelegate OnGameOver;
            public static event OnHealthChangedDelegate OnHurt;
        #endregion
        
        public int health;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Enemy enemy)) return;
            LoseHealth(enemy.damage);
            enemy.gameObject.SetActive(false);
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
