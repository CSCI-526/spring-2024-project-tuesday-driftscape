using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int decHealth = 10;
    public GameObject energyPrefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Hurt()
    {
        Debug.Log(1);
        Instantiate(energyPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.GetAttacked(decHealth); // 调用 GetAttacked 方法处理伤害
            }
        }
    }
}