using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTriggerSctipt : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("������");
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag != "Acammo"&& collision.gameObject.tag != "Dcammo" && collision.gameObject.tag != "Arm"&& collision.gameObject.tag != "Player" && collision.gameObject.tag != "Jump")
        {
            Destroy(this.gameObject);
        }
    }
    void OnBecameInvisible() //��ʊO�̒e�ۏ���
    {
        Destroy(this.gameObject);
    }
}
