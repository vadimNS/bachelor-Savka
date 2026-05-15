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

    private const string TARGET_SCENE = "MainGame"; // <-- ЗАМІНІТЬ НА НАЗВУ ВАШОЇ СЦЕНИ

    public void SceneTransition()
    {
        if (menuPanel != null) menuPanel.SetActive(false);
        if (loadingPanel != null) loadingPanel.SetActive(true);

        StartCoroutine(LoadSceneAsync(TARGET_SCENE));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Завантажуємо сцену асинхронно, дозволяємо активацію автоматично
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Якщо сцена не може завантажитися (наприклад, немає в Build), вийдемо
        if (operation == null)
        {
            Debug.LogError($"Не вдалося завантажити сцену: {sceneName}");
            yield break;
        }

        // Не забороняємо активацію — сцена сама перемкнеться, коли буде готова
        // operation.allowSceneActivation залишається true за замовчуванням

        while (!operation.isDone)
        {
            // progress іде від 0 до 1 (якщо allowSceneActivation = true)
            float progress = Mathf.Clamp01(operation.progress);

            if (loadingSlider != null)
                loadingSlider.value = progress;
            if (loadingText != null)
                loadingText.text = $"{progress * 100:F0}%";

            yield return null;
        }

        // Тут сцена вже завантажилась, UI зникне разом зі старою сценою
    }
}