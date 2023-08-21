using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("Ç†ÇΩÇË");
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible() //âÊñ äOÇÃíeä€èàóù
    {
        Destroy(this.gameObject);
    }
}
