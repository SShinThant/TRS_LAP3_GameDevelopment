using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SST
{
    public class PlayerStats : CharacterStats
    {
        /*public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;*/

        public static event Action OnPlayerDeath;

        public HealthBar healthBar;

        AnimatorHandler animatorHandler;

        /*[SerializeField]
        private float delayBeforeLoading = 5f;

        private float timeElapsed;*/

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
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

            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation("Damage", true);


            

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Dead", true);
                isDead = true;
                GetComponent<Collider>().enabled = false;
                OnPlayerDeath?.Invoke();
                //SceneManager.LoadScene("GameOverScene");
                
                /*timeElapsed += Time.deltaTime;

                if (timeElapsed > delayBeforeLoading && currentHealth <= 0)
                {
                    animatorHandler.PlayTargetAnimation("Dead", true);
                    SceneManager.LoadScene("GameOverScene");
                }*/
                
            }
        }

        public void HealPlayer(int healAmount)
        {
            currentHealth = currentHealth + healAmount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            healthBar.SetCurrentHealth(currentHealth);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Win")
            {
                SceneManager.LoadScene("VictoryScene");
            }        
        }
    }
}