using UnityEngine;
using System.Collections;

public class GameMenuScrpt : MonoBehaviour {

	public void LoadLevel(string LevelName)
    {
        Application.LoadLevel(LevelName);

    }

    public void ExitGame()
    {
        Application.Quit();

    }
}
