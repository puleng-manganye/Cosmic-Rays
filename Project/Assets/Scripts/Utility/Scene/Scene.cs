using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Class Description:
/*
 * 
 */
#endregion

public class Scene : MonoBehaviour
{
    #region Fields

    private bool loadSceneInProgress;

    #endregion

    #region Load scene

    // This function will load any scene
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        loadSceneInProgress = true;

        //Fade out screen
        UIFader fader = GameObject.FindObjectOfType<UIFader>();

        if (fader != null)
        {
            fader.Fade(UIFader.FADE.FadeOut, 0.4f, 0.4f);
        }

        yield return new WaitForSeconds(1f);

        //Load new scene
        SceneManager.LoadScene(sceneIndex);

        loadSceneInProgress = false;
    }
    #endregion

    #region Close program

    // This funtion will close the program
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You exited game!");
    }
    #endregion
}
