using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            Debug.Log("�����e�~��");
        }

        if (collision.gameObject.tag == "Dcammo")
        {
            Debug.Log("�����e�~��");
        }
    }

   
}
