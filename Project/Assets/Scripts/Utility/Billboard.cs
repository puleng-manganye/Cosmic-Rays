using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/*
 * This class rotates a Billboard to face the Camera
 */
#endregion

public class Billboard : MonoBehaviour
{
    #region Fields

    // A reference to the Camera the Billboard will be facing
    public Transform cam;
    #endregion

    #region Rotate Billboard to face Camera

    private void LateUpdate()
    {
        // Rotate Billboard to face the Camera
        transform.LookAt(transform.position + cam.forward);
    }
    #endregion
}