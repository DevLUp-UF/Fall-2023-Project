using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField]
    private PlayerInput map;
    public PlayerCharacter Player;
    private InputAction movement;
    private InputAction melee;
    private InputAction range;
    private InputAction aim;

    void Start()
    {
        movement = map.actions.FindAction("Movement");
        melee = map.actions.FindAction("Attack1");
        range = map.actions.FindAction("Attack2");
        aim = map.actions.FindAction("Aim");
    }

    public void Freeze()
    {
        movement.Disable();
        melee.Disable();
        range.Disable();
        aim.Disable();
        Time.timeScale = 0.0f;
    }

    public void Thaw()
    {
        movement.Enable();
        melee.Enable();
        range.Enable();
        aim.Enable();
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1.0f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
