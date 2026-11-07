using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    #region Class Description:
    /*
     * 
     */
    #endregion

    public class SetActive3 : MonoBehaviour
    {
        #region Fields

        // A reference to the Icon
        public GameObject Icon;
        #endregion

        #region References and Initializations

        private void Update()
        {
            // Disable Icon
            Icon.gameObject.SetActive(false);
        }
        #endregion
    }
