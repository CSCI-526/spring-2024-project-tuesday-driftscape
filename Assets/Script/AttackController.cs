using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int decHealth = 10;
    public GameObject energyPrefab;
    public int health = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if player tagged position distance < 1.05f
        // playercontroller getattacked
    }
    public void Hurt()
    {
        health -= 51;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(energyPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.GetAttacked(decHealth); // 调用 GetAttacked 方法处理伤害
            }
        }
    }
}