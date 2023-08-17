using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public float floorSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Acammo")
        {
            Debug.Log("°”»’è");
            floorSpeed += 1f;

            if(floorSpeed > 3)
            {
                floorSpeed = 3;
            }
        }
    }
}
