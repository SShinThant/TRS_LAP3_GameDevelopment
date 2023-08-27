using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SST
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;

        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                if (playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                }
            }

            if (collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                TurtleStats turtleStats = collision.GetComponent<TurtleStats>();
                SlimeStats slimeStats = collision.GetComponent<SlimeStats>();
                DragonBossStats dragonBossStats = collision.GetComponent<DragonBossStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                }

                if (turtleStats != null)
                {
                    turtleStats.TakeDamage(currentWeaponDamage);
                }

                if (slimeStats != null)
                {
                    slimeStats.TakeDamage(currentWeaponDamage);
                }

                if (dragonBossStats != null)
                {
                    dragonBossStats.TakeDamage(currentWeaponDamage);
                }
            }
        }


    }
}