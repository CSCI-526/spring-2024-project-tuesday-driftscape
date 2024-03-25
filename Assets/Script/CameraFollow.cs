using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player��Transform���
    public float yOffset = 4f; // ����ͷ�����Player�Ĵ�ֱƫ��

    void LateUpdate()
    {
        if (player != null)
        {
            // ��������ͷ��λ�ã�ʹ��ʼ��λ��Player�Ϸ�4����λ
            transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
        }
    }
}
