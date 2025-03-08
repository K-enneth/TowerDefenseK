using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Towers
{
    public class Tower : MonoBehaviour
    {
        public int cost;
        public int timeBetweenAttacks;
        public int damage;
        [SerializeField] private List<Enemy>enemiesInRange;
        private Coroutine _attackCoroutine;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Enemy enemy)) return;
            enemiesInRange.Add(enemy);
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(Attack());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Enemy enemy)) return;
            enemiesInRange.Remove(enemy);
            
            if (enemiesInRange.Count == 0 && _attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
        
        private IEnumerator Attack()
        {
            while (enemiesInRange.Count > 0)
            {
                enemiesInRange.RemoveAll(enemy => enemy.currentHealth <= 0);
                
                for (var indexEnemy = 0; indexEnemy < enemiesInRange.Count; indexEnemy++)
                {
                    if (enemiesInRange[indexEnemy] !=null)
                    {
                        enemiesInRange[indexEnemy].TakeDamage(damage);
                    }
                }
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
        }
    }
}
