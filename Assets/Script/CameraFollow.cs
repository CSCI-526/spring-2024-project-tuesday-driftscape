using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player��Transform���
    public Transform healthBar;
    public float yOffset = 4f; // ����ͷ�����Player�Ĵ�ֱƫ��
    private float xhpOffset = -10f;
    private float yhpOffset = -2f;

    void LateUpdate()
    {
        if (player != null)
        {
            // ��������ͷ��λ�ã�ʹ��ʼ��λ��Player�Ϸ�4����λ
            transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
            healthBar.position = new Vector3(player.position.x + xhpOffset, player.position.y + yhpOffset, healthBar.position.z);
        }
    }
}
