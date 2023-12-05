using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    Rigidbody2D Mrb;
    [SerializeField] public float Mspeed = -5f;
    private float defaultSpeed;

    Animator missileAnimator_normal;

    public AnimationClip missile_default;
    public AnimationClip missileAnimator_a;
    public AnimationClip missileAnimator_d;

    [SerializeField] private GameObject _destroyedPrefab;
    private Vector2 _destroyedPrefabPosition;
    private bool _isHitAcc;
    private bool _isHitDec;

    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = Mspeed;
        missileAnimator_normal = GetComponent<Animator>();
        Mrb = GetComponent<Rigidbody2D>();
        //Animator missileAnimator_normal = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Mrb.velocity = new Vector2(Mspeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo" && !_isHitAcc)
        {
            _isHitAcc = true;
            Invoke("AccAmmoHitWaitTime", 0.1f);
            Mspeed -= 2f;

            if (Mspeed < defaultSpeed)
            {
                missileAnimator_normal.Play(missileAnimator_a.name);

            }

            //missileAnimator_normal.Play(missileAnimator_a.name);
            /*if(Mspeed < defaultSpeed)
            {
                missileAnimator_normal.Play(missileAnimator_a.name);
            }*/

            if (Mspeed < -9f)
            {
                Mspeed = -9f;
            }
        }

        if (collision.gameObject.tag == "Dcammo" && !_isHitDec)
        {
            _isHitDec = true;
            Invoke("DecAmmoHitWaitTime", 0.1f);
            Mspeed += 2f;

            if (Mspeed > defaultSpeed)
            {
                missileAnimator_normal.Play(missileAnimator_d.name);

            }

            //missileAnimator_normal.Play(missileAnimator_d.name);
            /*if(Mspeed > defaultSpeed)
            {
                missileAnimator_normal.Play(missileAnimator_d.name);
            }*/

            if (Mspeed > -1f)
            {
                Mspeed = -1f;
            }
        }
        if (collision.gameObject.tag == "Acammo" || collision.gameObject.tag == "Dcammo")
        {
            if (Mspeed == defaultSpeed)
            {
                missileAnimator_normal.Play(missile_default.name);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            DestroyObject();
        }
    }

    private void AccAmmoHitWaitTime()
    {
        _isHitAcc = false;
    }

    private void DecAmmoHitWaitTime()
    {
        _isHitDec = false;
    }

    public void DestroyObject()
    {
        _destroyedPrefabPosition = transform.position;
        _destroyedPrefabPosition.x -= 2.5f;
        Instantiate(_destroyedPrefab, _destroyedPrefabPosition, transform.rotation = Quaternion.Euler(0, 0, -90));
        Destroy(gameObject);
    }
}
