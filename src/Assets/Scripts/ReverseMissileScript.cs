using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseMissileScript : MonoBehaviour
{
    Rigidbody2D Mrb;
    public float Mspeed = 5f;
    private float defaultSpeed;

    Animator missileAnimator_normal;

    public AnimationClip missile_default;
    public AnimationClip missileAnimator_a;
    public AnimationClip missileAnimator_d;

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

        /*if(Mspeed == defaultSpeed)
        {
            missileAnimator_normal.Play(missile_default.name);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            Mspeed += 2f;
            missileAnimator_normal.Play(missileAnimator_a.name);
            /*if(Mspeed < defaultSpeed)
            {
                missileAnimator_normal.Play(missileAnimator_a.name);
            }*/

            if (Mspeed < 15f)
            {
                Mspeed = 15f;
            }
        }

        if (collision.gameObject.tag == "Dcammo")
        {
            Mspeed -= 1f;
            missileAnimator_normal.Play(missileAnimator_d.name);
            /*if(Mspeed > defaultSpeed)
            {
                missileAnimator_normal.Play(missileAnimator_d.name);
            }*/

            if (Mspeed == 0)
            {
                Mspeed = 1f;
            }
        }
    }
}
