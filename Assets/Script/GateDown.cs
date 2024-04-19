using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDown : MonoBehaviour
{
    public GameObject Gate; // 指定要移动的门对象
    public float yOffset = 2.0f; // 定义Y坐标的偏移量

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的对象是否是Player
        if (collision.gameObject.tag == "Player")
        {
            // 移动Gate
            MoveObject(Gate);
        }
    }

    void MoveObject(GameObject objectToMove)
    {
        // 通用方法，更新任何游戏对象的位置
        if (objectToMove != null)
        {
            Debug.Log(1);
            Vector3 newPosition = objectToMove.transform.position;
            newPosition.y += yOffset; // 更新Y坐标
            objectToMove.transform.position = newPosition; // 应用新位置
            Vector3 newTriggerPosition = gameObject.transform.position;
            newTriggerPosition.y -= 0.5f; // 更新Y坐标
            gameObject.transform.position = newTriggerPosition; // 应用新位置
        }
    }
}
