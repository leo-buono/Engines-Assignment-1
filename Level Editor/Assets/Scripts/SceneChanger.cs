using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	static public bool mainGame = true;
	public static void StartGame()
    {
		mainGame = true;
        SceneManager.LoadScene("Main Level");
		CommandManager.instance.Clear();
    }

	public static void StartTutorial()
    {
		mainGame = false;
        SceneManager.LoadScene("Tutorial");
		CommandManager.instance.Clear();
    }

	public static void Retry()
	{
		if (mainGame)
			StartGame();
		else
			StartTutorial();
	}

    public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void LoadWin()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public static void LoadDed()
    {
        SceneManager.LoadScene("Ded Screen");
    }
	
    public static void quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
