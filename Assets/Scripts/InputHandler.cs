using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SST
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool p_Input;
        public bool t_Input;
        public bool v_Input;
        public bool la_Input;
        public bool ha_Input;
        public bool jump_Input;
        public bool inventory_Input;
        public bool lockOnInput;
        public bool leftLockOnInput;
        public bool rightLockOnInput;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool forwardstepFlag;
        public bool twoHandFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool lockOnFlag;
        public bool inventoryFlag;
        public float forwardstepInputTimer;
        

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        WeaponSlotManager weaponSlotManager;
        CameraHandler cameraHandler;
        AnimatorHandler animatorHandler;
        UIManager uiManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            //playerAttacker = GetComponent<PlayerAttacker>();
            playerAttacker = GetComponentInChildren<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            uiManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }



        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                inputActions.PlayerActions.LA.performed += i => la_Input = true;
                inputActions.PlayerActions.HA.performed += i => ha_Input = true;
                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;
                inputActions.PlayerActions.P.performed += i => p_Input = true;               
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
                inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
                inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => rightLockOnInput = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => leftLockOnInput = true;
                inputActions.PlayerActions.T.performed += i => t_Input = true;
                inputActions.PlayerActions.TalkToNPC.performed += i => v_Input = true;
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }


        public void TickInput(float delta)
        {
            HandleMoveInput(delta);
            HandleForwardStepInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotsInput();
            HandleInventoryInput();
            HandleLockOnIput();
            HandleTwoHandInput();
        }

        private void HandleMoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleForwardStepInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.triggered;
            sprintFlag = b_Input;

            if (b_Input)
            {
                forwardstepInputTimer += delta; 
            }
            else
            {
                if (forwardstepInputTimer > 0 && forwardstepInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    forwardstepFlag = true;
                }

                forwardstepInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            if (la_Input)
            {
                playerAttacker.HandleLAAction();
                /*if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;

                    if (playerManager.canDoCombo)
                        return;

                    animatorHandler.anim.SetBool("isUsingRightHand", true);
                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                }*/
            }

            if (ha_Input)
            {
                playerAttacker.HandleHAAction();
                //playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }

        private void HandleQuickSlotsInput()
        {
            if (d_Pad_Right)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if (d_Pad_Left)
            {
                playerInventory.ChangeLefttWeapon();
            }
        }

        private void HandleInventoryInput()
        {
            if (inventory_Input)
            {
                inventoryFlag = !inventoryFlag;

                if (inventoryFlag)
                {
                    uiManager.OpenSelectWindow();
                    uiManager.UpdateUI();
                    uiManager.hudWindow.SetActive(false);
                }
                else
                {
                    uiManager.CloseSelectWindow();
                    uiManager.CloseAllInventoryWindows();
                    uiManager.hudWindow.SetActive(true);
                }
            }
        }

        private void HandleLockOnIput()
        {
            if (lockOnInput &&  lockOnFlag == false)
            {                
                lockOnInput = false; 
                cameraHandler.HandleLockOn();
                if (cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if (lockOnInput && lockOnFlag)
            {
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }

            if (lockOnFlag && leftLockOnInput)
            {
                leftLockOnInput = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
            }

            if (lockOnFlag && rightLockOnInput)
            {
                rightLockOnInput = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.rightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }

            cameraHandler.SetCameraHeight();
        }

        private void HandleTwoHandInput()
        {
            if (t_Input)
            {
                t_Input = false; 

                twoHandFlag = !twoHandFlag;

                if (twoHandFlag)
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                }
                else
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);
                }
            }
        }

        
    }
}
