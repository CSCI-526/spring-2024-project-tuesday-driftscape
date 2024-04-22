using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAttackController : MonoBehaviour
{
    public int decHealth = 10;
    public GameObject energyPrefab;
    public int health = 100;

    // 移动时左右翻转用到
    private Vector3 lastPosition;

    void Start()
    {
        
        lastPosition = transform.position;
 
    }

    // Update is called once per frame
    void Update()
    {
        //if player tagged position distance < 1.05f
        // playercontroller getattacked

         // 计算敌人当前的移动方向
        Vector3 currentDirection = (transform.position - lastPosition).normalized;

        // 根据移动方向调整朝向
        if (currentDirection.x < 0)
        {
            //flip the sprite
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            
        }
        else if (currentDirection.x > 0)
        {
            //flip the sprite
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        // 更新上一帧的位置
        lastPosition = transform.position;

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