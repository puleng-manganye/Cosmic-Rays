using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/*
 * 
 */
#endregion

public class SetActiveTimer : MonoBehaviour
{
    #region Fields

    public bool g1Active = false;
    public bool g2Active = false;

    // Time to wait before destroying the gameObject
    public float waitTime = 4.0f;

    // Bool to automatically start Timer
    public bool autoStart;

    // A reference to the Triggers
    public GameObject gameObject1, gameObject2, gameObject3, gameObject4, gameObject5, gameObject6;

    #endregion

    #region Start Timer

    // Starts co-routines
    private void Update()
    {
        if (autoStart)
        {
            StartCoroutine(WaitTimer());
        }
    }
    #endregion

    #region Timer

    // Waits some time and then destroys the gameObject
    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(waitTime);

        if (g1Active == false && g1Active == false)
        {
            // Enable gameObject2/Disable gameObject1
            gameObject1.gameObject.SetActive(true);
            gameObject2.gameObject.SetActive(true);
            gameObject3.gameObject.SetActive(false);
            gameObject4.gameObject.SetActive(false);
            gameObject5.gameObject.SetActive(false);
            gameObject6.gameObject.SetActive(false);
        }
    }
    #endregion
}
