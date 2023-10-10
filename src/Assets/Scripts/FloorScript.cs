using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    float animationSpeed;  // �x���R���̃A�j���[�V�����̑��x

    public float floorSpeed = 0f;  // �x���R���ɏ�������̈ړ����x
    
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animationSpeed = animator.GetFloat("AnimationSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Acammo")
        {
            if(transform.rotation.y == 0)  // ���]
            {
                Debug.Log("������");
                floorSpeed += 1f;
                animationSpeed -= 0.3f;

                if (floorSpeed > 3)
                {
                    floorSpeed = 3;
                    animationSpeed = -0.9f;
                }

                animator.SetFloat("AnimationSpeed", animationSpeed);
            }
            else if (transform.rotation.y != 0)  // �t�]
            {
                Debug.Log("������");
                floorSpeed -= 1f;
                animationSpeed -= 0.3f;

                if (floorSpeed < -3)
                {
                    floorSpeed = -3;
                    animationSpeed = -0.9f;
                }

                animator.SetFloat("AnimationSpeed", animationSpeed);
            }
            /*Debug.Log("������");
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
            if (transform.rotation.y == 0)  // ���]
            {
                Debug.Log("������");
                floorSpeed -= 2.0f; //�������x����
                animationSpeed += 0.3f;

                if (floorSpeed < 0)
                {
                    floorSpeed = 0;
                    animationSpeed = 0;
                }

                animator.SetFloat("AnimationSpeed", animationSpeed);
            }
            else if (transform.rotation.y != 0)  // �t�]
            {
                Debug.Log("������");
                floorSpeed += 2.0f; //�������x����
                animationSpeed += 0.3f;

                if (floorSpeed > 0)
                {
                    floorSpeed = 0;
                    animationSpeed = 0;
                }

                animator.SetFloat("AnimationSpeed", animationSpeed);
            }

            /*Debug.Log("������");
            floorSpeed -= 2.0f; //�������x����
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
