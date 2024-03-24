using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutRope : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的对象是否是子弹
        if (collision.CompareTag("Bullet")) // 确保子弹有一个Tag标记为"Bullet"
        {
            Destroy(collision.gameObject); // 销毁子弹
            Destroy(gameObject); // 销毁当前挂载这个脚本的绳索对象
        }
    }
}
