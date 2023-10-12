using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject menuHolder;
    bool menuIsOpen = false;

    private void OnEnable()
    {
        menuHolder.gameObject.SetActive(false);
    }
    private void ChangeMenu()
    {
        menuHolder.SetActive(menuIsOpen);
        SoundManager.Instance.FlipGameState();
    }

    public void ToggleMenu()
    {
        if (!menuIsOpen)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }

    // Update is called once per frame
    public void PauseGame()
    {
        SoundManager.Instance.UIPauseOpen();
        menuIsOpen = true;
        ChangeMenu();
        GameManager.Instance.Freeze();
    }

    public void UnpauseGame()
    {
        SoundManager.Instance.UIPauseClose();
        menuIsOpen = false;
        ChangeMenu();
        GameManager.Instance.Thaw();
    }
}
