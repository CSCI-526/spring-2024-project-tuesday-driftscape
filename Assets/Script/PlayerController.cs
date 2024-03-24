using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    public float jumpForce; // 跳跃力度
    public float speed;
    public float originalSpeed = 5f; // 假定原始速度为5
    public KeyCode jumpKey = KeyCode.Space;
    private Rigidbody2D rb2d;

    public int health = 100;
    public int decHealth = 5;
    public int incHealth = 5;
    private float timer = 0f; // 计时器
    public GameObject fgoal;

    public Navigation[] navis;
    public Transform fbuild;

    // private LevelCompleteAnalytics analytic;


    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private bool isJump = true;

    private Color originalColor;
    public Transform[] enemies;
    public Transform[] dummyenemies;
    private bool canMoveFreely = false;
    public float FreeFlytime;


    public GameObject[] foods;


    private bool isPaused = false;
    private bool hasFake = false;
    public GameObject pauseMenuUI;

    public Time timestart;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取SpriteRenderer组件
        // analytic = GetComponent<LevelCompleteAnalytics>();
        originalColor = spriteRenderer.color; // 保存原始颜色
        /*success.SetActive(false);
        restart.SetActive(false);
        nextlevel.SetActive(false);*/
        pauseMenuUI.SetActive(false);
        jumpForce = 8;
        FreeFlytime = 3.0f;
        speed = 5;
        fgoal.SetActive(false);
    }

    void Update()
    {
        Debug.Log(health);
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = canMoveFreely ? Input.GetAxis("Vertical") : 0;
        if (!hasFake)
        {
            fbuild.position = transform.position;
        }
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     TogglePause();
        // }
        // 累加时间
        timer += Time.deltaTime;

        // 每当计时器达到或超过1秒时
        if (timer >= 1f)
        {
            // 减少生命值
            health -= 1;
            // 重置计时器
            timer = 0f;
        }
        if (canMoveFreely)
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
            rb2d.velocity = movement;
        }
        if (enemies.Length > 0)
        {
            foreach (Transform enemy in enemies)
            {
                if (Vector3.Distance(enemy.position, transform.position) < 1.05f)
                {
                    health -= 1;
                    StartCoroutine(FlashRed());
                }
            }
        }
        eatFood();
        if (dummyenemies.Length > 0)
        {
            foreach (Transform dummyenemy in dummyenemies)
            {
                if (Vector3.Distance(dummyenemy.position, transform.position) < 1.05f)
                {
                    health -= 1;
                    StartCoroutine(FlashRed());
                }
            }
        }
        Move();
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        // if input q, then player will lose gravity for 3 seconds
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TemporaryLoseGravity(FreeFlytime));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hasFake = true;
            fgoal.SetActive(true);
            foreach (Navigation navi in navis)
            {
                navi.getconfused = true;
            }
            StartCoroutine(FakeGoal(3f));
        }
        if (health <= 0)
        {
            Debug.Log("health=0");
            Time.timeScale = 0;
            //restart.SetActive(true);
        }
        // 如果重启的UI显示，并且玩家按下了F键，则重新加载当前场景
        /*if (restart.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            ReloadCurrentScene();
        }
        if (nextlevel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            float timeElapsed = Time.time;
            //analytic.SendLevelCompleteEvent(SceneManager.GetActiveScene().name, true, timeElapsed);
            ReloadNextScene();
        }*/
    }
    void Jump()
    {
        isJump = true;
        Vector2 v = new Vector2(Physics2D.gravity.x, -Physics2D.gravity.y / 9.8f);
        rb2d.AddForce(v * jumpForce, ForceMode2D.Impulse);

    }
    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        rb2d.velocity = movement;
    }
    void eatFood()
    {
        if (foods.Length > 0)
        {
            foreach (GameObject food in foods)
            {
                if (food.activeSelf && Vector3.Distance(food.transform.position, transform.position) < 0.8f)
                {
                    Debug.Log("before" + health);
                    health = Math.Min(incHealth + health, 100);
                    food.SetActive(false);
                    Debug.Log("after" + health);
                }
            }
        }
    }
    void ReloadCurrentScene()
    {
        Time.timeScale = 1; // 场景运动
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // 获取当前场景的索引
        SceneManager.LoadScene(sceneIndex); // 根据索引重新加载场景
        //possession = 0;
        Vector2 originalGravity = Physics2D.gravity;
        Physics2D.gravity = new Vector2(0, -9.81f);
    }
    void ReloadNextScene()
    {
        Time.timeScale = 1; // 场景运动
        //SceneManager.LoadScene(nextsceneName); // 加载指定场景
        //possession = 0;
        Vector2 originalGravity = Physics2D.gravity;
        Physics2D.gravity = new Vector2(0, -9.81f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Enter ground");
            isGrounded = true; // 接触地面时更新地面状态
            isJump = true;

        }
        if (other.gameObject.CompareTag("Goal")) // 检测是否碰撞到Goal
        {
            spriteRenderer.color = Color.green; // 将球体颜色改为绿色
            Time.timeScale = 0; // 静止场景
            /*success.SetActive(true);
            nextlevel.SetActive(true);*/
        }
        if (other.gameObject.CompareTag("Slow")) // 碰撞到 "Slow" 地板
        {
            speed = originalSpeed * 0.5f; // 速度减半
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        // Continuously check for collision with the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJump = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // 离开地面时更新地面状态
        }
        if (other.gameObject.CompareTag("Goal")) // 检测是否碰撞到Goal
        {
            spriteRenderer.color = originalColor; // 将球体颜色改为原本颜色
        }
        if (other.gameObject.CompareTag("Slow")) // 离开 "Slow" 地板
        {
            speed = originalSpeed; // 速度恢复
        }
    }
    IEnumerator TemporaryLoseGravity(float duration)
    {
        rb2d.gravityScale = 0; // 玩家失去重力
        canMoveFreely = true; // 允许玩家自由移动
        rb2d.velocity = new Vector2(rb2d.velocity.x, 20f);
        //hintransform.anchoredPosition += hintransform.rect.width/2*Vector2.right;
        yield return new WaitForSeconds(duration); // 等待指定时间
        rb2d.gravityScale = 1; // 恢复重力
        canMoveFreely = false; // 恢复正常移动限制
        health -= decHealth;
    }

    IEnumerator FakeGoal(float duration)
    {
        foreach (Navigation navi in navis)
        {
            navi.agent.SetDestination(fbuild.position);
            yield return new WaitForSeconds(duration);
            navi.getconfused = false;
            fgoal.SetActive(false);
            hasFake = false;
            health -= decHealth;
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.03f);
        spriteRenderer.color = originalColor;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("撞到了导弹");
    }
}