using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player的Transform组件
    public Transform healthBar;
    public float yOffset = 4f; // 摄像头相对于Player的垂直偏移
    private float xhpOffset = -10f;
    private float yhpOffset = -2f;

    void LateUpdate()
    {
        if (player != null)
        {
            // 更新摄像头的位置，使其始终位于Player上方4个单位
            transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
            healthBar.position = new Vector3(player.position.x + xhpOffset, player.position.y + yhpOffset, healthBar.position.z);
        }
    }
}
