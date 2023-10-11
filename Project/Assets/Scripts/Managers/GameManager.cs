using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public PlayerCharacter Player;

    public void ExitGame()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
    }
}
