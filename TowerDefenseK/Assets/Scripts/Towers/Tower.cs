using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Towers
{
    public class Tower : MonoBehaviour
    {
        #region Variables
            public int cost;
            public int timeBetweenAttacks;
            public int damage;
            [SerializeField] private List<Enemy>enemiesInRange;
            private Coroutine _attackCoroutine;
            private ParticleSystem _explosion;
        #endregion


        private void Start()
        {
            _explosion = gameObject.GetComponentInChildren<ParticleSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            //Adds enemies in range to the list of enemies it can hurt
            if (!other.TryGetComponent(out Enemy enemy)) return;
            enemiesInRange.Add(enemy);
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(Attack());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Removes ememies from list once they leave range
            if (!other.TryGetComponent(out Enemy enemy)) return;
            enemiesInRange.Remove(enemy);
            
            if (enemiesInRange.Count == 0 && _attackCoroutine != null)
            {
                //Stops attacking when there's no enemies in range
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
        
        private IEnumerator Attack()
        {
            while (enemiesInRange.Count > 0)
            {
                //Removes enemies who are already dead to avoid infinite money
                enemiesInRange.RemoveAll(enemy => enemy.currentHealth <= 0);
                
                for (var indexEnemy = 0; indexEnemy < enemiesInRange.Count; indexEnemy++)
                {
                    if (enemiesInRange[indexEnemy] !=null)
                    {
                        _explosion.Play();
                        enemiesInRange[indexEnemy].TakeDamage(damage);
                    }
                }
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
        }
    }
}
