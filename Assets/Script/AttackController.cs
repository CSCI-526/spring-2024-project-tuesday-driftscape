using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int decHealth = 10;
    public GameObject energyPrefab;
    public int health = 100;

    // 移动时左右翻转用到
    private Vector3 lastPosition;

    public Animator animator;


    private float lastBeingHurtTime;

    void Start()
    {
        
        lastPosition = transform.position;
        // 动画组件
        animator = GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
        animator.SetBool("isBeingHurt", false);
        animator.SetBool("isWaiting", false);

        Time.timeScale = 1;
        lastBeingHurtTime = Time.time;
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
            transform.localScale = new Vector3(1, 1, 1); // 向左
        }
        else if (currentDirection.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 向右
        }

        // 更新上一帧的位置
        lastPosition = transform.position;


        if(Time.time - lastBeingHurtTime > 1.0f){
            animator.SetBool("isWaiting", false);
            animator.SetBool("isBeingHurt", false);
        }
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
            animator.SetBool("isAttacking", true);
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision) //触碰到别的碰撞器
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("isBeingHurt!");
            animator.SetBool("isBeingHurt", true);
            animator.SetBool("isWaiting", true);
            lastBeingHurtTime = Time.time;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", false);
        }
    }


}