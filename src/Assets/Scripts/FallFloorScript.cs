using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloorScript : MonoBehaviour
{
    private Rigidbody2D FFrb;
    [SerializeField] public float FallSpeed = -3f;
  public  bool isFall;
    // Start is called before the first frame update
    void Start()
    {
        FFrb = this.gameObject.GetComponent<Rigidbody2D>();
        isFall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFall == true)
        {
            Vector2 fallVelocity = new Vector2 (FFrb.velocity.x, FallSpeed);
            FFrb.velocity = fallVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Jump")
        //{
        //    Invoke("FallFloor", 3); //3�b��
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jump")
        {
            Invoke("FallFloor", 3); //3�b��
        }
        //if(collision.gameObject.tag == "Acammo")
        //{
        //    FallSpeed -= 1f;

        //    if(FallSpeed <-6f)
        //    {
        //        FallSpeed = -6f;
        //    }
        //}

        //if(collision.gameObject.tag == "Dcammo")
        //{
        //    FallSpeed += 1f;

        //    if(FallSpeed >1f)
        //    {
        //        FallSpeed = 1f;
        //    }
        //}
    }

    void FallFloor()
    {
        isFall = true;
    }
}
