using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("level1");
    }


    public void CompleteLevel()
    {
        // Assuming the current scene is Level 1, load Level 2
        // You may want to add logic to check the current level and load accordingly

        SceneManager.LoadScene("level2");
    }
}
