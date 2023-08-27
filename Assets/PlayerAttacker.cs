using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SST
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        PlayerManager playerManager;
        PlayerStats playerStats;
        PlayerInventory playerInventory;
        InputHandler inputHandler;
        public string lastAttack;

        private void Awake()
        {
            animatorHandler = GetComponent<AnimatorHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            inputHandler = GetComponentInParent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    AudioManager.instance.Play("SwordSlash");
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                }
                else if (lastAttack == weapon.TH_Light_Attack_01)
                {
                    AudioManager.instance.Play("SwordSlash");
                    animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_02, true);
                }
            }     
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            if (inputHandler.twoHandFlag)
            {
                AudioManager.instance.Play("SwordSlash");
                animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_01, true);
                lastAttack = weapon.TH_Light_Attack_01;
            }
            else
            {
                AudioManager.instance.Play("SwordSlash");
                animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
                lastAttack = weapon.OH_Light_Attack_1;
            }          
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            AudioManager.instance.Play("SwordSlash");
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            lastAttack = weapon.OH_Heavy_Attack_1;
            /*if (inputHandler.twoHandFlag)
            {

            }
            else
            {
                animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
                lastAttack = weapon.OH_Heavy_Attack_1;
            }*/
        }

        #region Input Action

        public void HandleLAAction()
        {
            if (playerInventory.rightWeapon.isMeleeWeapon)
            {
                PerformLAMeleeAction();
            }
            else if (playerInventory.rightWeapon.isSpellCaster || playerInventory.rightWeapon.isFaithCaster || playerInventory.rightWeapon.isPyroCaster)
            {
                AudioManager.instance.Play("HealSpell");
                PerformLAMagicAction(playerInventory.rightWeapon);
            }     
        }

        public void HandleHAAction()
        {
            if (playerInventory.rightWeapon.isMeleeWeapon)
            {
                PerformHAMeleeAction();
            }
            
        }

        #endregion


        #region Attack Action

        private void PerformLAMeleeAction()
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;

                animatorHandler.anim.SetBool("isUsingRightHand", true);
                HandleLightAttack(playerInventory.rightWeapon);
            }
        }

        private void PerformHAMeleeAction()
        {
            HandleHeavyAttack(playerInventory.rightWeapon);
        }
        

        private void PerformLAMagicAction(WeaponItem weapon)
        {
            if (weapon.isFaithCaster)
            {
                if(playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
                {
                    playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats);
                }
            }
        }

        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler, playerStats);
        }

        #endregion
    }
}