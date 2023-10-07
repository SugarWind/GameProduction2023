using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMachineScript : MonoBehaviour
{
    [SerializeField] private Transform TopPos;
    Rigidbody2D rb;

    public float MoveSpeed = 3.0f;
    int direction = -1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        rb.MovePosition(new Vector2(transform.position.x, transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction));

        if (transform.position.y > TopPos.position.y)
        {
            direction *= -direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            direction = -direction;
            Debug.Log("プレス接触");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dcammo")
        {
            MoveSpeed -= 0.5f;

            Debug.Log("プレス減速");

            if (MoveSpeed < 1f)
            {
                MoveSpeed = 1f;
            }
        }
    }

}
