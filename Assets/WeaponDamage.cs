using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SST
{
    public class WeaponDamage : MonoBehaviour
    {
        public int damage = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
    }
}