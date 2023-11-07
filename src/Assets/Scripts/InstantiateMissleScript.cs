using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMissleScript : MonoBehaviour
{
    [SerializeField] private GameObject MissleOb;
    [SerializeField] private float MissleRate;
    [SerializeField] private float StartMissle;
    [SerializeField] private float DetectionRange = 20f;

    private GameObject player;
    private bool isPlayerNear;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckIsPlayerNear", StartMissle, MissleRate);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInRange();
    }

    void CheckPlayerInRange()
    {
        /*if (Vector3.Distance(transform.position, player.transform.position) < DetectionRange)
        {
            isPlayerNear = true;
        }
        else
        {
            isPlayerNear = false;
        }*/

        isPlayerNear = (Vector3.Distance(transform.position, player.transform.position) < DetectionRange ? true : false);
    }

    void CheckIsPlayerNear()
    {
        if(isPlayerNear)
        {
            CreateMissle();
        }
    }

    void CreateMissle()
    {
      GameObject mP =  Instantiate(MissleOb, transform.position, Quaternion.identity);
    }
}
