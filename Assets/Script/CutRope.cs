using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutRope : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�Ķ����Ƿ����ӵ�
        if (collision.CompareTag("Bullet")) // ȷ���ӵ���һ��Tag���Ϊ"Bullet"
        {
            Destroy(collision.gameObject); // �����ӵ�
            Destroy(gameObject); // ���ٵ�ǰ��������ű�����������
        }
    }
}
