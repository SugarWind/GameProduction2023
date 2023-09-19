using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed =3f;
    public float Pspeed = 1;
    public float floorSpeed = 0f;
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
            rb.velocity = new Vector2(speed+floorSpeed, rb.velocity.y);
        }
       else  if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new  Vector2(-speed+floorSpeed, rb.velocity.y);
        } 
        //else
        //{
        //    Pspeed = 0;
        //}
    }

 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            Debug.Log("è∞Ç∆ÉvÉåÉCÉÑÅ[");
            FloorScript flscript = collision.gameObject.GetComponent<FloorScript>();
            floorSpeed = flscript.floorSpeed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") //è∞Ç©ÇÁó£ÇÍÇΩÇÁ
        {
            floorSpeed = 0f;
        }
    }
}


