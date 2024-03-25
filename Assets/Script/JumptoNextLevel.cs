using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumptoNextLevel : MonoBehaviour
{
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("level2");
    }

}
