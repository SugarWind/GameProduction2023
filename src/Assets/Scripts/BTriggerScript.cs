using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTriggerScript : MonoBehaviour
{
    [SerializeField] private AnimationClip _accAnimation;
    [SerializeField] private AnimationClip _explosionAnimation;
    [SerializeField] private float _explosionSize = 0.5f;
    private GameObject _effectObject;
    private Animator _accAnimator;
    private Rigidbody _rb;
    private CircleCollider2D _col2D;

    void Start()
    {
        _effectObject = transform.Find("bullet_acc_effect").gameObject;
        _accAnimator = GetComponent<Animator>();
        _col2D = this.gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            CircleCollider2D circleCollider = this.GetComponent<CircleCollider2D>();
            rb.velocity = Vector2.zero;
            circleCollider.radius = _explosionSize;
            Destroy(_effectObject);
            _accAnimator.SetTrigger("collisionTrigger");
        }

        if (collision.gameObject.tag != "Acammo" && collision.gameObject.tag != "Dcammo" && collision.gameObject.tag != "Arm" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Jump" && collision.gameObject.tag != "Trigger")
        {
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            CircleCollider2D circleCollider = this.GetComponent<CircleCollider2D>();
            rb.velocity = Vector2.zero;
            circleCollider.radius = _explosionSize;
            Destroy(_effectObject);
            _accAnimator.SetTrigger("collisionTrigger");
        }
    }

    void OnBecameInvisible() //画面外の弾丸処理
    {
        Destroy(this.gameObject);
    }

    public void OnAnimationColFinish()
    {
        _col2D.enabled = false;
    }

    public void OnAnimationFinish()
    {
        Destroy(this.gameObject);
    }
}
