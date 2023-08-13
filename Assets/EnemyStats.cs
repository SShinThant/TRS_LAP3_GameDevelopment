using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SST
{
    public class EnemyStats : MonoBehaviour
    {
        /*public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;*/

        //public bool isDead;

        public Animator animator;
        

        private int HP = 100;
        public Slider healthBar;

        

        

        void Update()
        {
            healthBar.value = HP;
        }

        

        //Animator animator;

        /*private void Awake()
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
        }*/

        public void TakeDamage(int damage)
        {
            /*if (isDead)
                return;*/

            //currentHealth = currentHealth - damage;           
            //animator.Play("Damage");

            //maxHealth -= damage;

            /*if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Dead");
                isDead = true;
            }*/

            HP -= damage;
            if (HP <= 0)
            {
                animator.SetTrigger("die");
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                animator.SetTrigger("damage");
            }
        }

        
    }
}
