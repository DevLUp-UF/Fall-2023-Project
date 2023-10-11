using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : SingletonBehaviour<UIManager>
{
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private InputActionReference pause;

    private void Update()
    {
        if (pause.action.WasPerformedThisFrame())
        {
            pauseMenu.ToggleMenu();
        }
    }
}
