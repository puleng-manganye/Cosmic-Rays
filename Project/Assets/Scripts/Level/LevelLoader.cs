using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#region Class Description:
/*
 * 
 */
#endregion

public class LevelLoader : MonoBehaviour
{
    #region Fields

    // Reference to our loading screen
    public GameObject loadingScreen;

    // UI slider
    public Slider slider;

    // UI text element for the loading progress
    public Text progressText;

    // Index of screen to load
    public int sceneIndex;
    #endregion

    #region Load scene asyncronously
    public void Update()
    {
        StartCoroutine(LoadAsyncronously(sceneIndex));
    }

    IEnumerator LoadAsyncronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 1f);

            //Debug.Log(progress);
            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
    #endregion
}
