using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SST
{
    public class SlimeStats : MonoBehaviour
    {
        public Animator animator;


        private int HP = 100;
        public Slider healthBar;


        void Update()
        {
            healthBar.value = HP;
        }


        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                AudioManager.instance.Play("SkeletonDeath");
                animator.SetTrigger("dead");
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                AudioManager.instance.Play("SkeletonDamage");
                animator.SetTrigger("slashed");
            }
        }
    }
}