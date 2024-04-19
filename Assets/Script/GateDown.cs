using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDown : MonoBehaviour
{
    public GameObject Gate; // ָ��Ҫ�ƶ����Ŷ���
    public float yOffset = 2.0f; // ����Y�����ƫ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�Ķ����Ƿ���Player
        if (collision.gameObject.tag == "Player")
        {
            // �ƶ�Gate
            MoveObject(Gate);
        }
    }

    void MoveObject(GameObject objectToMove)
    {
        // ͨ�÷����������κ���Ϸ�����λ��
        if (objectToMove != null)
        {
            Debug.Log(1);
            Vector3 newPosition = objectToMove.transform.position;
            newPosition.y += yOffset; // ����Y����
            objectToMove.transform.position = newPosition; // Ӧ����λ��
            Vector3 newTriggerPosition = gameObject.transform.position;
            newTriggerPosition.y -= 0.5f; // ����Y����
            gameObject.transform.position = newTriggerPosition; // Ӧ����λ��
        }
    }
}
