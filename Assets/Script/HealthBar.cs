using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using TMPro;  // TextMeshPro -> HP Value Text
using UnityEngine;
using UnityEngine.UI;



public class HealthBar : MonoBehaviour
{
    public Slider healthBar;                // HP Slider
    public Image fakeImg;                   // fake CD fill image
    public float fakeCD = 6.0F;             // fake CD 
    public Image flyImg;                    // fly CD fill image
    public float flyCD = 6.0F;              // fly CD
    public Image shootImg;                  // shoot CD fill image
    public float shootCD = 2.0F;            // shoot CD 

    public PlayerController playerController;

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

            flyImg.fillAmount = 0F;
            //UnityEngine.Debug.Log(cd);

        }
        else
        {
            flyImg.fillAmount = (playerController.nextfly - Time.time) / flyCD;
            //UnityEngine.Debug.Log(cd);

        }
        if (playerController.nextfake < Time.time)
        {
            fakeImg.fillAmount = 0F;
        }
        else
        {
            fakeCD = 6.0F;
            fakeImg.fillAmount = (playerController.nextfake - Time.time + 0.001F) / fakeCD;
        }
        if (playerController.nextFire<Time.time)
        {
            shootImg.fillAmount = 0F;
        }
        else
        {
            shootImg.fillAmount = (playerController.nextFire - Time.time) / shootCD;
        }

    }
}
