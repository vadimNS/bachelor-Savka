using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private Text loadingText;

    private const string TARGET_SCENE = "MainGame"; 

    public void SceneTransition()
    {
        if (menuPanel != null) menuPanel.SetActive(false);
        if (loadingPanel != null) loadingPanel.SetActive(true);

        StartCoroutine(LoadSceneAsync(TARGET_SCENE));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        
        if (operation == null)
        {
            Debug.LogError($"Не вдалося завантажити сцену: {sceneName}");
            yield break;
        }

        
        

        while (!operation.isDone)
        {
            
            float progress = Mathf.Clamp01(operation.progress);

            if (loadingSlider != null)
                loadingSlider.value = progress;
            if (loadingText != null)
                loadingText.text = $"{progress * 100:F0}%";

            yield return null;
        }

        
    }
}