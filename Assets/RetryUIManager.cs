using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SST
{
    public class RetryUIManager : MonoBehaviour
    {
        public GameObject gameOverMenu;

        private void OnEnable()
        {
            PlayerStats.OnPlayerDeath += EnableGameOverMenu;
        }

        private void OnDisable()
        {
            PlayerStats.OnPlayerDeath -= EnableGameOverMenu;
        }

        public void EnableGameOverMenu()
        {
            gameOverMenu.SetActive(true);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene("ForestScene");
        }
    }
}