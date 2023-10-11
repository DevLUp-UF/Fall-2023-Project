using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    [SerializeField]
    private PauseMenu pauseMenu;

    public void Init()
    {
        ShowPauseMenu(false);
    }

    public void ShowPauseMenu(bool shouldShow = true)
    {
        pauseMenu.EnableMenu(shouldShow);
    }
}
