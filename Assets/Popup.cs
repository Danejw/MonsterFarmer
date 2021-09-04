using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;


namespace VRMarket.UI
{
    public class Popup : MonoBehaviour
    {
        public string popupName = "Send";
        public bool autoHide;
        public float autoHideDelay;


        public void ShowPopup(string name)
        {
            //get a clone of the UIPopup, with the given PopupName, from the UIPopup Database 
            UIPopup popup = UIPopup.GetPopup(popupName);

            //make sure that a popup clone was actually created
            if (popup == null) return;

            if (autoHide)
            {
                popup.AutoHideAfterShow = true;
                popup.AutoHideAfterShowDelay = autoHideDelay;
            }


            popup.Show(); //show the popup
        }
        
    }
}
