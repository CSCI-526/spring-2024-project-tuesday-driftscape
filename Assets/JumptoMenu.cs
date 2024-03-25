using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumptoMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
