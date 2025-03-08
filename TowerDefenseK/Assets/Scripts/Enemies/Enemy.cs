using Managers;
using UnityEngine;

namespace Enemies
{
   public class Enemy : MonoBehaviour
   {
      #region Variables
         public int startHealth;
         public int currentHealth;
         public int damage;
         public int points;
         private FollowWaypoints _followWaypoints;
      #endregion

      #region Delegate and Event
         public delegate void OnDeath();
         public static event OnDeath DeadEvent;
      #endregion

      public void OnEnable()
      {
         currentHealth = startHealth;
         gameObject.GetComponent<SphereCollider>().enabled = true;
      }

      public void OnDisable()
      {
         gameObject.GetComponent<SphereCollider>().enabled = false;
         _followWaypoints.currentWaypoint = 0;
      }

      private void Awake()
      {
         _followWaypoints = GetComponent<FollowWaypoints>();
      }

      public void TakeDamage(int damaged)
      {
         currentHealth -= damaged;
         if (currentHealth <= 0)
         {
            Die();
         }
      }

      private void Die()
      {
         //Adds money when killed
         CoinManager.instance.AddCoin(points);
         DeadEvent?.Invoke();
         gameObject.SetActive(false);
      }
      
   }
}
