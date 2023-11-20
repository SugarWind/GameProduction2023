using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject _destroyedPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("あたり");
            Instantiate(_destroyedPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag != "Dcammo" && collision.gameObject.tag != "Acammo" && collision.gameObject.tag != "Arm" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Jump" && collision.gameObject.tag != "Trigger")
        {
            Instantiate(_destroyedPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible() //画面外の弾丸処理
    {
        Destroy(this.gameObject);
    }
}
