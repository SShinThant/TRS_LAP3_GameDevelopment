using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SST
{
    public class TurtleStats : MonoBehaviour
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
                animator.SetTrigger("death");
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                AudioManager.instance.Play("SkeletonDamage");
                animator.SetTrigger("hit");
            }
        }
    }
}