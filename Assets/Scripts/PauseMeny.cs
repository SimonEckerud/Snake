using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMeny : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;


    public GameObject PauseMenyUI; 

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartMeny");
        Time.timeScale = 1f;

    }

    public void Resume()
    {
        PauseMenyUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit!");
    }
    void Pause()
    {
        PauseMenyUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
