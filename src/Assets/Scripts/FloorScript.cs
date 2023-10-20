using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    float animationSpeed;  // ベルコンのアニメーションの速度

    public float floorSpeed = 0f;  // ベルコンに乗った時の移動速度

    private float defaultSpeed;

    Animator animator_normal;
    public AnimationClip animator_default;
    public AnimationClip animator_a;
    public AnimationClip animator_d;

    // Start is called before the first frame update
    void Start()
    {
        animator_normal = GetComponent<Animator>();
        defaultSpeed = floorSpeed;
        //animationSpeed = animator.GetFloat("AnimationSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        /*if(floorSpeed == defaultSpeed)
        {
            animator_normal.Play(animator_default.name);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            animator_normal.Play(animator_a.name);

            if (transform.rotation.y == 0)  // 正転
            {
                Debug.Log("床判定");
                floorSpeed += 1f;
                animationSpeed -= 0.3f;

                /*if(floorSpeed > defaultSpeed)
                {
                    animator_normal.Play(animator_a.name);
                }*/

                if (floorSpeed > 3)
                {
                    floorSpeed = 3;
                    animationSpeed = -0.9f;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }
            else if (transform.rotation.y != 0)  // 逆転
            {
                Debug.Log("床判定");
                floorSpeed -= 1f;
                animationSpeed -= 0.3f;

                /*if (floorSpeed < defaultSpeed)
                {
                    animator_normal.Play(animator_a.name);
                }*/

                if (floorSpeed < -3)
                {
                    floorSpeed = -3;
                    animationSpeed = -0.9f;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }
            /*Debug.Log("床判定");
            floorSpeed += 1f;
            animationSpeed -= 0.3f;

            if(floorSpeed > 3)
            {
                floorSpeed = 3;
                animationSpeed = -0.9f;
            }

            animator.SetFloat("AnimationSpeed", animationSpeed);*/
        }

        if (collision.gameObject.tag == "Dcammo")
        {
            animator_normal.Play(animator_d.name);

            if (transform.rotation.y == 0)  // 正転
            {
                Debug.Log("床減速");
                floorSpeed -= 2.0f; //落下速度減衰
                animationSpeed += 0.3f;

                /*if (floorSpeed < defaultSpeed)
                {
                    animator_normal.Play(animator_d.name);
                }*/

                if (floorSpeed < 0)
                {
                    floorSpeed = 0;
                    animationSpeed = 0;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }
            else if (transform.rotation.y != 0)  // 逆転
            {
                Debug.Log("床減速");
                floorSpeed += 2.0f; //落下速度減衰
                animationSpeed += 0.3f;

                /*if (floorSpeed > defaultSpeed)
                {
                    animator_normal.Play(animator_d.name);
                }*/

                if (floorSpeed > 0)
                {
                    floorSpeed = 0;
                    animationSpeed = 0;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }

            /*Debug.Log("床減速");
            floorSpeed -= 2.0f; //落下速度減衰
            animationSpeed += 0.3f;

            if( floorSpeed < 0)
            {
                floorSpeed = 0;
                animationSpeed = 0;
            }

            animator.SetFloat("AnimationSpeed", animationSpeed);*/
        }
    }
}
