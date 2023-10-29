using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloorScript : MonoBehaviour
{
    private Rigidbody2D FFrb;
    [SerializeField] private float FallSpeed = -2f;
    bool isFall;
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
        if (collision.gameObject.tag == "Player")
        {
            isFall = true;
        }
    }
}
