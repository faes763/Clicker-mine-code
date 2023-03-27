using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public float speed = 180f;
    private Rigidbody2D rb;
    private int move = 1;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * move, 0);
    }
    void OnTriggerEnter2D (Collider2D other) {
        if(other.CompareTag("TargetBar")) {
            move = move * (-1);
        }
    }
    void setSpeed(int newSpeed) {
        speed = newSpeed;   
    }
}
