using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player��Transform���
    public Transform healthBar;
    public Transform coolDown;
    public Transform coolDown2;
    public Transform coolDown3;
    public float yOffset = 4f; // ����ͷ�����Player�Ĵ�ֱƫ��
    private float xhpOffset = -10f;
    private float yhpOffset = -2f;
    private float xcdOffset = -7.6f;
    private float ycdOffset = -0f;
    private float xcdOffset2 = -7.6f;
    private float ycdOffset2 = -2f;
    private float xcdOffset3 = -7.6f;
    private float ycdOffset3 = -4f;

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
            healthBar.position = new Vector3(player.position.x + xhpOffset, player.position.y + yhpOffset, healthBar.position.z);
            coolDown.position = new Vector3(player.position.x + xcdOffset, player.position.y + ycdOffset, coolDown.position.z);
            coolDown2.position = new Vector3(player.position.x + xcdOffset2, player.position.y + ycdOffset2, coolDown2.position.z);
            coolDown3.position = new Vector3(player.position.x + xcdOffset3, player.position.y + ycdOffset3, coolDown3.position.z);
        }
    }
}
