using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuHolder;


    public void EnableMenu(bool enable)
    {
        menuHolder.SetActive(enable);
    }

    // Update is called once per frame
    public void PauseGame()
    {
        EnableMenu(true);
        Time.timeScale = 0.0f;
    }

    public void UnpauseGame()
    {
        EnableMenu(false);
        Time.timeScale = 1.0f;
    }
}
