using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    float animationSpeed;

    public float floorSpeed = 0f;
    
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
            Debug.Log("è∞îªíË");
            floorSpeed += 1f;
            animationSpeed -= 0.3f;

            if(floorSpeed > 3)
            {
                floorSpeed = 3;
                animationSpeed = -0.9f;
            }

            animator.SetFloat("AnimationSpeed", animationSpeed);
        }

        if (collision.gameObject.tag == "Dcammo")
        {
            Debug.Log("è∞å∏ë¨");
            floorSpeed -= 2.0f; //óéâ∫ë¨ìxå∏êä
            animationSpeed += 0.3f;

            if( floorSpeed < 0)
            {
                floorSpeed = 0;
                animationSpeed = 0;
            }

            animator.SetFloat("AnimationSpeed", animationSpeed);
        }
    }
}
