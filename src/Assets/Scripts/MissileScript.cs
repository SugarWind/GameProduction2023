using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    Rigidbody2D Mrb;
    private float Mspeed = -5f;
    // Start is called before the first frame update
    void Start()
    {
         Mrb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Mrb.velocity = new Vector2(Mspeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Acammo")
        {
            Mspeed -= 2f;

            if(Mspeed < -15f)
            {
                Mspeed = -15f;
            }
        }

        if(collision.gameObject.tag =="Dcammo")
        {
            Mspeed += 1f;

            if(Mspeed > 0)
            {
                Mspeed = -1f;
            }
        }

       
    }
}
