using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBed : MonoBehaviour
{
    public float jumpForce = 10f;
    private float jumpCooldownTimer = 0f; // ��Ծ��ȴ��ʱ��
    private bool canJump = true; // �����Ƿ�������Ծ
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpCooldownTimer += Time.deltaTime;
        // �����ʱ������0.5�룬���ñ�����������Ծ
        if (jumpCooldownTimer >= 0.5f)
        {
            canJump = true;
            jumpCooldownTimer = 0f; // ���ü�ʱ��
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canJump)
        {
            anim.SetTrigger("jump");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            canJump = false; // ��Ծ��������ֹ��һ����Ծ
        }
    }
}
