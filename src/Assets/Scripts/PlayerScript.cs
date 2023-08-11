using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed =3f;
    public float Pspeed = 1;

     Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) &&rb.velocity.y == 0)
        {
            rb.AddForce(transform.up * 300);

        }
    }
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
       else  if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new  Vector2(-speed, rb.velocity.y);
        } 
        //else
        //{
        //    Pspeed = 0;
        //}
    }

}
