using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�
using UnityEngine.SceneManagement; // ���볡�������

public class UIButton : MonoBehaviour
{
    public Button Home;
    public Button Restart;
    public Button Next;
    public string nextLevel;

    void Start()
    {
        Home.onClick.AddListener(OnClickButton1);
        Restart.onClick.AddListener(OnClickButton2);
        Next.onClick.AddListener(OnClickButton3);
    }

    void OnClickButton1()
    {

        SceneManager.LoadScene("MainMenu");
    }

    void OnClickButton2()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    void OnClickButton3()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
