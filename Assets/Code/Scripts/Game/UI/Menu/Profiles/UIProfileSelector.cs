﻿namespace ProjectPBR.Managers.Static.Profiles
{
    using UnityEngine;

    public class UIProfileSelector : MonoBehaviour
    {
        private void Start()
        {
            ProfileSelector.TrySelectFirstProfile(); // Select first profile if there is one, to trigger event
        }
    }
}
