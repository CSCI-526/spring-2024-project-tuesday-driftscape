using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hurt()
    {
        // Debug.Log("I died!");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other){

    }
}