using UnityEngine;

#region Class Description:
/*
 * 
 */
#endregion

public class OpenURL : MonoBehaviour
{
    #region Fields

    public string websiteAddress;
    #endregion

    #region Open URL

    public void OpenURLOnClick()
    {
        Application.OpenURL(websiteAddress);
    }
    #endregion
}
