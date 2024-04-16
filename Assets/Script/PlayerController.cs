using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;



public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5.0f; // ��Ծ����
    public float speed;
    public float originalSpeed = 5f; // �ٶ�ԭʼ�ٶ�Ϊ5
    public KeyCode jumpKey = KeyCode.Space;
    private Rigidbody2D rb2d;

    public int maxHealth = 100;
    public int health = 100;
    public int decHealth = 5;
    public int incHealth = 5;
    private float timer = 0f; // ��ʱ��
    //public GameObject fgoal;

    public float nextfly = 1.5f;
    public float flygap = 15.0f;
    public float nextfake = 0.0F;
    public Navigation[] navis;
    public GameObject fakePrefab;
    //public Transform fbuild;

    // private LevelCompleteAnalytics analytic;

    private LevelCompleteAnalytics analytic;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;

    private Color originalColor;
    public Transform[] dummyenemies;
    public Vector2[] dummyenemyPos;
    private bool canMoveFreely = false;
    public float FreeFlytime;
    public int flytimes;
    public int faketimes;


    // private bool isPaused = false;
    private bool hasFake = false;
    private GameObject fake;
    // public GameObject pauseMenuUI;

    // Bullet
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Transform where bullets will be instantiated


    public float timestart;
    // 当前帧计数器
    private int frameCounter = 0;

    // UI
    public GameObject homeButton;
    public GameObject restartButton; // 重新开始按钮
    public GameObject nextButton;


    public string sceneName;
    public Animator animator;
    public void MyFunctionToCall()
    {
        Debug.Log("Called by Animation Event!");
    }

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if(sceneName=="tutorial_enemy" || sceneName == "tutorial_move"|| sceneName == "tutorial_skill2"){
            flygap = 1000.0f;
        }
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        foreach (AudioListener listener in listeners) {
            if (listener.gameObject != this.gameObject) {
                Destroy(listener); // or listener.enabled = false; to disable
            }
        }

        timestart = Time.time;
        Time.timeScale = 1;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��ȡSpriteRenderer���
        originalColor = spriteRenderer.color; // ����ԭʼ��ɫ
        dummyenemyPos = new Vector2[dummyenemies.Length];
        for (int i = 0; i < dummyenemies.Length; i++)
        {
            dummyenemyPos[i] = dummyenemies[i].position;
        }

        speed = 5;
        //fgoal.SetActive(false);
        restartButton.SetActive(false);
        homeButton.SetActive(false);
        nextButton.SetActive(false);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(animator!=null){
            animator.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = canMoveFreely ? Input.GetAxis("Vertical") : 0;
        // if (!hasFake)
        // {
        //     fbuild.position = transform.position;
        // }
        timer += Time.deltaTime;


        // ÿ����ʱ���ﵽ�򳬹�1��ʱ
        if (timer >= 1f)
        {
            health -= 1;
            timer = 0f;
        }
        if (canMoveFreely)
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;
            rb2d.velocity = movement;
        }
        if (dummyenemies.Length >= 0)
        {
            // dummy enemy move left and right around its original position
            foreach (Transform dummyenemy in dummyenemies)
            {
                if (dummyenemy != null)
                {
                    dummyenemy.position = dummyenemyPos[Array.IndexOf(dummyenemies, dummyenemy)] + new Vector2(Mathf.Sin(Time.time), 0) * 3;
                }
            }

            foreach (Transform dummyenemy in dummyenemies)
            {
                if (dummyenemy != null)
                {
                    if (Vector3.Distance(dummyenemy.position, transform.position) < 1.05f)
                    {
                        health -= 1;
                        StartCoroutine(FlashRed());
                    }
                }
            }
        }
        Move();
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        if (isGrounded)
        {
            if(animator!=null){
                animator.SetBool("jump", false);
            }
        }
        // if input q, then player will lose gravity for 3 seconds
        if (Input.GetKeyDown(KeyCode.E)&& Time.time > nextfly&&sceneName!="tutorial_enemy" && sceneName != "tutorial_move"&& sceneName != "tutorial_skill2")
        {
            nextfly = Time.time + flygap;
            Vector3 newPosition = transform.position;
            newPosition.y += 1.0f;
            transform.position = newPosition;
            StartCoroutine(TemporaryLoseGravity(FreeFlytime));
            flytimes++;
        }
        if (Input.GetKeyDown(KeyCode.Q) && !hasFake && Time.time > nextfake)
        {
            nextfake = Time.time + 6.0F;
            hasFake = true;
            //fgoal.SetActive(true);
            fake = Instantiate(fakePrefab, transform.position, transform.rotation);
            foreach (Navigation navi in navis)
            {
                if (navi && navi.agent.isActiveAndEnabled){
                    navi.getconfused = true;
                    navi.agent.SetDestination(fake.transform.position);
                    navi.agent.speed = 1.5f;
                } 
            }
            StartCoroutine(FakeGoal(3f));
            TakeDamage(decHealth);
            faketimes++;
        }
        if (health <= 0 || Input.GetKeyDown(KeyCode.Escape))
        {
            restartButton.SetActive(true); // 显示重新开始按钮
            homeButton.SetActive(true); // 显示重新开始按钮
            Time.timeScale = 0;
            float timeElapsed = Time.time;
            float locationX = transform.position.x;
            float locationY = transform.position.y;
            analytic.SendLevelCompleteEvent(SceneManager.GetActiveScene().name, true, timeElapsed, flytimes, faketimes, health, locationX, locationY);
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Shoot();
        }

    }
    void Jump()
    {
        Debug.Log("Jump");
        Vector2 v = new Vector2(Physics2D.gravity.x, -Physics2D.gravity.y / 9.8f);
        rb2d.AddForce(v * jumpForce, ForceMode2D.Impulse);
        animator.SetBool("jump", true);
    }
    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if(moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        rb2d.velocity = movement;
    }

    public void AddHealth(int amount)
    {
        health = Mathf.Min(health + amount, 100);
       // Debug.Log("AddHealth, health:" + health);
    }
    public void GetAttacked(int amount)
    {
        TakeDamage(amount);
        StartCoroutine(FlashRed());
    }
    void ReloadCurrentScene()
    {
        Time.timeScale = 1; // �����˶�
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // ��ȡ��ǰ����������
        timestart = Time.time;
        SceneManager.LoadScene(sceneIndex); // �����������¼��س���
        //possession = 0;
        Vector2 originalGravity = Physics2D.gravity;
        Physics2D.gravity = new Vector2(0, -9.81f);
    }
    void ReloadNextScene()
    {
        Time.timeScale = 1; // �����˶�
        //SceneManager.LoadScene(nextsceneName); // ����ָ������
        //possession = 0;
        Vector2 originalGravity = Physics2D.gravity;
        Physics2D.gravity = new Vector2(0, -9.81f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

        }
        if (other.gameObject.CompareTag("Goal")) // ����Ƿ���ײ��Goal
        {
            spriteRenderer.color = Color.green; // ��������ɫ��Ϊ��ɫ
            Time.timeScale = 0; // ��ֹ����
            nextButton.SetActive(true); // 显示重新开始按钮
            homeButton.SetActive(true); // 显示重新开始按钮
            float timeElapsed = Time.time - timestart;
            float locationX = transform.position.x;
            float locationY = transform.position.y;
            analytic.SendLevelCompleteEvent(SceneManager.GetActiveScene().name, true, timeElapsed, flytimes, faketimes, health, locationX, locationY);


        }
        if (other.gameObject.CompareTag("Slow")) // ��ײ�� "Slow" �ذ�
        {
            speed = originalSpeed * 0.5f; // �ٶȼ���
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        // Continuously check for collision with the ground
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Hurt"))
        {
            isGrounded = true;
        }
        // 检查是否与Hurt标签的对象保持碰撞
        if (other.gameObject.CompareTag("Hurt")|| other.gameObject.CompareTag("Enemy"))
        {
            // 多帧减少一次生命值
            if (frameCounter % 4 == 0)
            {
                TakeDamage(1);
            }
            frameCounter++;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Hurt"))
        {
            isGrounded = false; // �뿪����ʱ���µ���״̬
        }
        if (other.gameObject.CompareTag("Goal")) // ����Ƿ���ײ��Goal
        {
            spriteRenderer.color = originalColor; // ��������ɫ��Ϊԭ����ɫ
        }
        if (other.gameObject.CompareTag("Slow")) // �뿪 "Slow" �ذ�
        {
            speed = originalSpeed; // �ٶȻָ�
        }

    }
    IEnumerator TemporaryLoseGravity(float duration)
    {
        rb2d.gravityScale = 0; // 设置无重力
        canMoveFreely = true; // 允许自由移动
        health -= decHealth; // 减少健康值（或类似的属性）
        rb2d.velocity = new Vector2(rb2d.velocity.x, 20f); // 设置刚体的向上速度

        yield return new WaitForSeconds(duration); // 等待指定时间

        StartCoroutine(FlashRedDuration(0.5f, 3)); // 开始闪烁红色效果，总时长0.5秒，闪烁3次
        yield return new WaitForSeconds(0.5f); // 等待红色闪烁效果完成

        rb2d.gravityScale = 1; // 恢复重力
        canMoveFreely = false; // 恢复移动限制
    }

    IEnumerator FlashRedDuration(float totalDuration, int flashCount)
    {
        float singleFlashDuration = totalDuration / (flashCount * 2); // 每次变色的持续时间

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = Color.red; // 更改为红色
            yield return new WaitForSeconds(singleFlashDuration); // 持续一段时间
            spriteRenderer.color = originalColor; // 恢复原色
            yield return new WaitForSeconds(singleFlashDuration); // 持续一段时间
        }
    }



    IEnumerator FakeGoal(float duration)
    {
        yield return new WaitForSeconds(duration);
        foreach (Navigation navi in navis)
        {
            if (navi && navi.agent.isActiveAndEnabled){
                navi.getconfused = false;
            }
        }
        // fgoal.SetActive(false);
        Destroy(fake);
        Debug.Log("Destroy");
        hasFake = false;
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = originalColor;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(FlashRed());
    }


    GameObject bullet;
    public float nextFire = 0.0F;
    public float fireCD = 2.0f;
    void Shoot()
    {
        // 子弹方向
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure z-coordinate is appropriate for 2D

        
        Vector2 direction = (mousePosition - transform.position).normalized;

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireCD;//子弹时间间隔设置为3.0秒
            bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            health -= decHealth;
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.SetDirection(direction);
            }
        }
    }



}