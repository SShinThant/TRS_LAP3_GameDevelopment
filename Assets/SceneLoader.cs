using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject load_UI;
    public Slider slider;
    

    public void LoadScene(int index)
    {
        StartCoroutine(LoadScene_Coroutine(index));
    }

    public IEnumerator LoadScene_Coroutine(int index)
    {
        slider.value = 0;
        load_UI.SetActive(true);
        

        AsyncOperation asynOperation = SceneManager.LoadSceneAsync(index);
        asynOperation.allowSceneActivation = false;
        float progress = 0;
        while (asynOperation.isDone) 
        {
            progress = Mathf.MoveTowards(progress, asynOperation.progress, Time.deltaTime);
            slider.value = progress;
            if (progress > 0.9f)
            {
                slider.value = 1;
                asynOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
