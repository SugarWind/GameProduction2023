using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTriggerScript : MonoBehaviour
{
    [SerializeField] private AnimationClip _decAnimation;
    [SerializeField] private AnimationClip _explosionAnimation;
    private GameObject _effectObject;
    private Animator _decAnimator;
    private Rigidbody _rb;
    void Start()
    {
        _effectObject = transform.Find("bullet_dec_effect").gameObject;
        _decAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("Ç†ÇΩÇË");
            Destroy(_effectObject);
            _decAnimator.SetTrigger("collisionTrigger");
        }

        if (collision.gameObject.tag != "Dcammo" && collision.gameObject.tag != "Acammo" && collision.gameObject.tag != "Arm" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Jump" && collision.gameObject.tag != "Trigger")
        {
            Destroy(_effectObject);
            _decAnimator.SetTrigger("collisionTrigger");
        }
    }

    void OnBecameInvisible() //âÊñ äOÇÃíeä€èàóù
    {
        Destroy(this.gameObject);
    }

    void OnAnimationFinish()
    {
        Destroy(this.gameObject);
    }
}
