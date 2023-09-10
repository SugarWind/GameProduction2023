using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    Rigidbody2D Mrb;
    // Start is called before the first frame update
    void Start()
    {
         Mrb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Mrb.velocity = new Vector2(-5, 0);
    }
}
