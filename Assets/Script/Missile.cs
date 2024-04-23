using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // 减少玩家的生命值
                playerController.TakeDamage(damage); // 假设每次撞击减少10点生命值
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Ground")  || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
