using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTop : MonoBehaviour
{
    private MissileScript _missileScript;
    // Start is called before the first frame update
    void Start()
    {
        _missileScript = transform.parent.gameObject.GetComponent<MissileScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            _missileScript.DestroyObject();
        }
    }
}
