using System.Collections;
using System.Collections.Generic;
using TMPro;  // TextMeshPro -> HP Value Text
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider healthBar;   // HP Slider
    public PlayerController playerController;
    public Transform coolDown;
    public Transform coolDown2;
    public Transform coolDown3;
    public TextMeshProUGUI healthText; // HP Text

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = playerController.maxHealth;
        healthBar.value = playerController.health;
        healthText.text = $"{playerController.health}/{playerController.maxHealth}";

        if (playerController.nextfly<Time.time)
        {
            coolDown.gameObject.SetActive(true);
        }
        else
        {
            coolDown.gameObject.SetActive(false);
        }
        if (playerController.nextfake < Time.time)
        {
            coolDown2.gameObject.SetActive(true);
        }
        else
        {
            coolDown2.gameObject.SetActive(false);
        }
        if(playerController.nextFire<Time.time)
        {
            coolDown3.gameObject.SetActive(true);
        }
        else
        {
            coolDown3.gameObject.SetActive(false);
        }

    }
}
