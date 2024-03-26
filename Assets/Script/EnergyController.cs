using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    public int incHealth = 10; // add health
    public GameObject player; 

    void Update()
    {
        EatFood();
    }

    void EatFood()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && gameObject.activeSelf && Vector3.Distance(transform.position, player.transform.position) < 1.05f)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.AddHealth(incHealth); // 调用 AddHealth 方法增加生命值
                gameObject.SetActive(false);
            }
        }
    }
}
