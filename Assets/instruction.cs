using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instruction : MonoBehaviour
{
    

    [SerializeField] public GameObject InstructionPanel;
    [SerializeField] public GameObject MovementPanel;
    [SerializeField] public GameObject AtkPanel;
    [SerializeField] public GameObject HealingPanel;
    [SerializeField] public GameObject InventoryPanel;
    [SerializeField] public GameObject EquipmentPanel;
    [SerializeField] public GameObject InteractionPanel;

    public void Instruction()
    {
        InstructionPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Movement()
    {
        MovementPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Atk()
    {
        AtkPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Heal()
    {
        HealingPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Inventory()
    {
        InventoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Equipment()
    {
        EquipmentPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Interaction()
    {
        InteractionPanel.SetActive(true);
        Time.timeScale = 0f;
    }


}
