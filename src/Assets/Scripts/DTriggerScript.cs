using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("あたり");
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible() //画面外の弾丸処理
    {
        Destroy(this.gameObject);
    }
}
