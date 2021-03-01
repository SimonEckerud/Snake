using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectLevel : MonoBehaviour
{

    
    // Start is called before the first frame update
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }

    public void Random()
    {
        int random = UnityEngine.Random.Range(1,SceneManager.sceneCount +2);
        SceneManager.LoadScene(random);
    }
}
