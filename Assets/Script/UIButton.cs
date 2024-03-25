using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class UIButton : MonoBehaviour
{
    public Button Home;
    public Button Restart;
    public Button Next;

    void Start()
    {
        Home.onClick.AddListener(OnClickButton1);
        Restart.onClick.AddListener(OnClickButton2);
        Next.onClick.AddListener(OnClickButton3);
    }

    void OnClickButton1()
    {
        Debug.Log("Button 1 was clicked.");
        // ���������Button1���߼�
    }

    void OnClickButton2()
    {
        Debug.Log("Button 2 was clicked.");
        // ���������Button2���߼�
    }

    void OnClickButton3()
    {
        Debug.Log("Button 3 was clicked.");
        // ���������Button3���߼�
    }
}
