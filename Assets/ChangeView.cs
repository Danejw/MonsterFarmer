using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy;

namespace Monster
{
    public class ChangeView : MonoBehaviour
    {
        // Views
        public GameObject view_stats;
        public GameObject view_actions;

        public void ToggleViews()
        {
            if (view_stats.activeSelf)
            {
                view_stats.SetActive(false);
                view_actions.SetActive(true);
            }
            else if (view_actions.activeSelf)
            {
                view_actions.SetActive(false);
                view_stats.SetActive(true);
            }
        }
    }
}
