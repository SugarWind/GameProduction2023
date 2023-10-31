using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    [SerializeField] private float animationSpeed;  // ベルコンのアニメーションの速度
    [SerializeField] public float normal_floorSpeed = 3f;  // ベルコンに乗った時の移動速度
    [SerializeField] public float reverse_floorSpeed = -3f;  // ベルコンに乗った時の移動速度
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float minSpeed = 1f;
    private float normal_defaultSpeed;  // ベルコンの初期スピード
    private float reverse_defaultSpeed;  // ベルコンの初期スピード

    Animator animator_normal;
    public AnimationClip animator_default;
    public AnimationClip animator_a;
    public AnimationClip animator_d;

    // Start is called before the first frame update
    void Start()
    {
        animator_normal = GetComponent<Animator>();
        normal_defaultSpeed = normal_floorSpeed;
        reverse_defaultSpeed = reverse_floorSpeed;

        if (transform.rotation.y == 0)
        {
            animationSpeed = -0.6f;
        }
        else if (transform.rotation.y != 0)
        {
            animationSpeed = 0.6f;
        }
        animator_normal.SetFloat("AnimationSpeed", animationSpeed);
        //animationSpeed = animator.GetFloat("AnimationSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        if (normal_floorSpeed == normal_defaultSpeed && transform.rotation.y == 0 ||
            reverse_floorSpeed == reverse_defaultSpeed && transform.rotation.y != 0)
        {
            animator_normal.Play(animator_default.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            //animator_normal.Play(animator_a.name);

            if (transform.rotation.y == 0)  // 正転
            {
                Debug.Log("床判定");
                normal_floorSpeed += 1f;
                animationSpeed -= 0.3f;

                if (normal_floorSpeed > normal_defaultSpeed)
                {
                    animator_normal.Play(animator_a.name);
                }

                /*if(floorSpeed > defaultSpeed)
                {
                    animator_normal.Play(animator_a.name);
                }*/

                if (normal_floorSpeed > maxSpeed)
                {
                    normal_floorSpeed = maxSpeed;
                    animationSpeed = -1.2f;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }
            else if (transform.rotation.y != 0)  // 逆転
            {
                Debug.Log("床判定");
                reverse_floorSpeed -= 1f;
                animationSpeed -= 0.3f;

                if (reverse_floorSpeed < reverse_defaultSpeed)
                {
                    animator_normal.Play(animator_a.name);
                }

                /*if (floorSpeed < defaultSpeed)
                {
                    animator_normal.Play(animator_a.name);
                }*/

                if (reverse_floorSpeed < -maxSpeed)
                {
                    reverse_floorSpeed = -maxSpeed;
                    animationSpeed = -1.2f;
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
            //animator_normal.Play(animator_d.name);

            if (transform.rotation.y == 0)  // 正転
            {
                Debug.Log("床減速");
                normal_floorSpeed -= 1f; //落下速度減衰
                animationSpeed += 0.3f;

                if (normal_floorSpeed < normal_defaultSpeed)
                {
                    animator_normal.Play(animator_d.name);
                }

                /*if (floorSpeed < defaultSpeed)
                {
                    animator_normal.Play(animator_d.name);
                }*/

                if (normal_floorSpeed < minSpeed)
                {
                    normal_floorSpeed = minSpeed;
                    animationSpeed = -0.1f;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }
            else if (transform.rotation.y != 0)  // 逆転
            {
                Debug.Log("床減速");
                reverse_floorSpeed += 1f; //落下速度減衰
                animationSpeed -= 0.3f;     /////

                if (reverse_floorSpeed > reverse_defaultSpeed)
                {
                    animator_normal.Play(animator_d.name);
                }

                /*if (floorSpeed > defaultSpeed)
                {
                    animator_normal.Play(animator_d.name);
                }*/

                if (reverse_floorSpeed > -minSpeed)
                {
                    reverse_floorSpeed = -minSpeed;
                    animationSpeed = 0.1f;
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
