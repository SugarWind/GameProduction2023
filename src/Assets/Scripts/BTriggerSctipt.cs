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
    }
    void OnBecameInvisible() //��ʊO�̒e�ۏ���
    {
        Destroy(this.gameObject);
    }
}
