using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMeny : MonoBehaviour
{
    public GameObject EndMenuUI;

    public static bool GameIsOver = false;

    public int showSore;
    private TextMeshProUGUI score_Text;


   


    
    
        
    

    public void ShowScore()
    {
        showSore = FindObjectOfType<GameplayController>().scoreCount;
        GameObject.Find("ScoreUI").SetActive(false);

        score_Text = GameObject.Find("ShowScore").GetComponent<TextMeshProUGUI>();

        score_Text.text = $"{showSore}";
    }

    public void Menu()
    {
        if (!GameIsOver)
        {
            Time.timeScale = 1f;
            EndMenuUI.SetActive(false);
            SceneManager.LoadScene("StartMeny");
            
        }

    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit!");
    }

    public void Restart()
    {
        if(!GameIsOver)
        {
            EndMenuUI.SetActive(false);
            
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
       
    }
}
