using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed = 5f;//子弹速度
    public Rigidbody2D rig;

    void Start () {
        Destroy(gameObject, 4); //2秒后销毁子弹，否则子弹会无限多
    }	
 
    // Controller中将direction传入这里
    public void SetDirection(Vector2 direction){
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) //触碰到别的碰撞器
    {
        
        Debug.Log("tag = " + collision.gameObject.tag);
        
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Gun"){ 
            // 情形1：碰到Player
            // do nothing
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            // 情形2：碰到敌人
            Debug.Log("Hit Enemy!");
            collision.gameObject.GetComponent<AttackController>().Hurt();
            //调用敌人的受伤函数
            Destroy(gameObject);
            
        }
        else
        {
            // 情形3： 碰到其他（墙壁等）
            Destroy(gameObject);
        }
        
    }


    void Update()
    {
        
    }


    
}

