using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        menuHolder.gameObject.SetActive(false);
    }

    [SerializeField]
    private GameObject menuHolder;
    bool menuIsOpen = false;

    private void ChangeMenu()
    {
        menuHolder.SetActive(menuIsOpen);
        SoundManager.Instance.FlipGameState();
    }

    public void ToggleMenu()
    {
        menuIsOpen = !menuIsOpen;
        ChangeMenu();
    }

    // Update is called once per frame
    public void PauseGame()
    {
        menuIsOpen = true;
        ChangeMenu();
        Time.timeScale = 0.0f;
    }

    public void UnpauseGame()
    {
        menuIsOpen = false;
        ChangeMenu();
        Time.timeScale = 1.0f;
    }
}
