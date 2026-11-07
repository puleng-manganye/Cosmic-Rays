using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class destroys a game object that it is attached to after a set length of time.
 */
#endregion

public class WaitDestroy : MonoBehaviour
{
    #region Fields

    [Header("Timing")]

    // Bool to automatically start Timer
    public bool autoStart;

    // Time to wait before destroying the gameObject
    public float waitTime = 1.0f;
    #endregion

    #region Start Timer

    // Starts co-routines
    private void Start()
    {
        if (autoStart)
        {
            StartCoroutine(WaitTimer());
        }
    }
    #endregion

    #region Start Timer from an external scripts

    // Since we can't start co-routines directly from external scripts,
    // this function allows us to do so, since public functions can be called from other scripts. 
    public void StartWaitTimer()
    {
        StartCoroutine(WaitTimer());
    }
    #endregion

    #region Timer

    // Waits some time and then destroys the gameObject
    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
    #endregion
}