using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;
    public void pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    
}
