using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadWin()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public void LoadDed()
    {
        SceneManager.LoadScene("Ded Screen");
    }
    public void quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
