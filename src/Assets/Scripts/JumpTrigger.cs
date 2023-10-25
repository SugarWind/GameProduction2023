using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    // �W�����v�����ǂ���
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // �^�O���FUntagged �ɓ������Ă���ԁi���n���Ă���ԁj
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
        else if (collision.gameObject.tag == "Missile")
        {
            isJumping = false;
        }
        if (collision.gameObject.tag == "Rmissile")
        {
            isJumping = false;
        }
        else if (collision.gameObject.tag == "floor")
        {
            isJumping = false;
        }
        else if (collision.gameObject.tag == "Untagged")
        {
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �^�O���FUntagged ���痣�ꂽ��i�n���痣��Ă���ԁj
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
        else if (collision.gameObject.tag == "Missile")
        {
            isJumping = true;
        }
        if (collision.gameObject.tag == "Rmissile")
        {
            isJumping = true;
        }
        else if (collision.gameObject.tag == "floor")
        {
            isJumping = true;
        }
        else if (collision.gameObject.tag == "Untagged")
        {
            isJumping = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            isJumping = false;
        }
        
        if(collision.gameObject.tag == "Rmissile")
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            isJumping = true;
        }

        if (collision.gameObject.tag == "Rmissile")
        {
            isJumping = true;
        }
    }
}
