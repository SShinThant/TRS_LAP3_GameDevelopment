using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SST
{
    public class DamageByEnemy : MonoBehaviour
    {

        public int damage = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }

    }
}