using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SST
{
    public class EnemyStats : CharacterStats
    {
        /*public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;*/

        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;

            currentHealth = currentHealth - damage;

            animator.Play("Damage");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Dead");
                isDead = true;
                //death animation
            }
        }
    }
}
