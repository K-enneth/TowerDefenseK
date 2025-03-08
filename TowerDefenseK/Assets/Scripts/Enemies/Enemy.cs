using System;
using Managers;
using UnityEngine;

namespace Enemies
{
   public class Enemy : MonoBehaviour
   {
      public int startHealth;
      public int currentHealth;
      public int damage;
      public int points;
      private FollowWaypoints _followWaypoints;

      public delegate void OnDeath();
      public static event OnDeath DeadEvent;

      public void OnEnable()
      {
         currentHealth = startHealth;
         gameObject.GetComponent<SphereCollider>().enabled = true;

      }

      private void Start()
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
         CoinManager.instance.AddCoin(points);
         DeadEvent?.Invoke();
         gameObject.GetComponent<SphereCollider>().enabled = false;
         gameObject.SetActive(false);
         _followWaypoints.currentWaypoint = 0;
      }

      public void End()
      {
         gameObject.SetActive(false);
         _followWaypoints.currentWaypoint = 0;
      }
      
   }
}
