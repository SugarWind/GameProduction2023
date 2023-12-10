using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMissleScript : MonoBehaviour
{
    [SerializeField] private GameObject MissleOb;
    [SerializeField] private float MissleRate;
    [SerializeField] private float StartMissle;
    [SerializeField] private float DetectionRange = 20f;
    [SerializeField] bool isReverse;

    private GameObject player;
    private bool isPlayerNear;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckIsPlayerNear", StartMissle, MissleRate);
        player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
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

    public void CreateMissle()
    {
        _animator.SetTrigger("startShot");
    }

    public void FinishAnimation()
    {
        Vector2 missilePos = this.transform.position;
        if (isReverse)
        {
            missilePos.x -= 3.5f;
        }
        else missilePos.x += 3.5f;
        GameObject mP = Instantiate(MissleOb, missilePos, Quaternion.identity);
    }
}
