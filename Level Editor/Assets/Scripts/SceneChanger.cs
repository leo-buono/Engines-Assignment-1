using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public static void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
		CommandManager.instance.Clear();
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
