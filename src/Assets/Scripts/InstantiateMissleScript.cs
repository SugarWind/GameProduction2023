using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMissleScript : MonoBehaviour
{
    [SerializeField] private GameObject MissleOb;
    [SerializeField] private float MissleRate = 5f; 
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateMissle", 1f, MissleRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMissle()
    {
      GameObject mP =  Instantiate(MissleOb,transform.position,Quaternion.identity);
    }
}
