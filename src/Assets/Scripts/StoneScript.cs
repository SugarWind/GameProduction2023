using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            Debug.Log("â¡ë¨íeÅ~ä‚");
        }

        if (collision.gameObject.tag == "Dcammo")
        {
            Debug.Log("å∏ë¨íeÅ~ä‚");
        }
    }

   
}
