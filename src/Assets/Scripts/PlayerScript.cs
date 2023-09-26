using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int jumpForce;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float floorSpeed = 0f;
    [SerializeField] private float flashInterval = 0.04f;
    [SerializeField] private float knockbackForce = 200.0f;
    [SerializeField] private float invincibleInterval = 2.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private bool isHit;
    private bool isJumping;
    public bool isInvincible;
    private bool isFacingRight = true;  // キャラの向きを管理
    private bool hitDirection = true;  // 攻撃がきた方向を管理

    public AnimationClip jumpRightClip;  // 右向きジャンプアニメーション
    public AnimationClip jumpLeftClip;   // 左向きジャンプアニメーション
    public AnimationClip runRightClip;  // 右向き移動アニメーション
    public AnimationClip runLeftClip;   // 左向き移動アニメーション
    public AnimationClip standRightClip;  // 右向き停止アニメーション
    public AnimationClip standLeftClip;  // 左向き停止アニメーション
    public AnimationClip damageRightClip;  // 右から当たったアニメーション
    public AnimationClip damageLeftClip;  // 左から当たったアニメーション

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isHit = false;
        isJumping = false;
        isInvincible = false;
    }

    private void Update()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        // ジャンプ
        if (Input.GetKeyDown(KeyCode.W) && !isJumping && !isHit)
        {
            Jump();
        }

        // アニメーション設定
        UpdateAnimations(moveDirection);
    }

    private void UpdateAnimations(float moveDirection)
    {
        // ジャンプ中かを更新
        // キャラの垂直方向の速度が0でない場合、true
        isJumping = rb.velocity.y != 0;

        // キャラの向きを更新
        if (moveDirection > 0.1f && !isJumping)
        {
            isFacingRight = true;
        }
        else if (moveDirection < -0.1f && !isJumping)
        {
            isFacingRight = false;
        }


        // ジャンプアニメーションを設定
        if (isJumping && !isHit)
        {
            // isFacingRightがtrueならjumpRightClip.nameで指定したアニメーションを再生、falseならjumpLeftClip.name
            animator.Play(isFacingRight ? jumpRightClip.name : jumpLeftClip.name);
        }
        else if (!isJumping && !isHit)
        {
            // 移動アニメーションを設定
            if (moveDirection != 0)
            {
                animator.Play(isFacingRight ? runRightClip.name : runLeftClip.name);
            }
            // 停止アニメーションを設定
            else
            {
                animator.Play(isFacingRight ? standRightClip.name : standLeftClip.name);
            }
        }
        // ダメージアニメーションを設定
        else if (isHit)
        {
            // 攻撃が右から来たならキャラの向きを右向きに、左からなら左向きに
            isFacingRight = hitDirection;
            animator.Play(isFacingRight ? damageLeftClip.name : damageRightClip.name);
        }
    }

    private void FixedUpdate()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        if (!isHit)
        {
            rb.velocity = new Vector2(moveDirection * speed + floorSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // 敵とぶつかったら
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            // ノックバックさせる方向の計算
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            Knockback(knockbackDirection);  // ノックバックさせる
            StartCoroutine(Flash());  // 点滅させる
            StartCoroutine(Invincible());  // 無敵状態にする
        }
    }

    private void Knockback(Vector2 direction)
    {
        // directionベクトルのxが正ならfalse、負ならtrue
        hitDirection = direction.x < 0;

        rb.AddForce(transform.right * (hitDirection ? -knockbackForce : knockbackForce));
    }

    private IEnumerator Flash()
    {
        // isHitがtrueの間は動けない
        isHit = true;

        for (int i = 0; i < 45; i++)
        {
            yield return new WaitForSeconds(flashInterval);
            spriteRenderer.enabled = !spriteRenderer.enabled;

            if (i == 10)
            {
                isHit = false;
            }
        }
        spriteRenderer.enabled = true;
    }

    private IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleInterval); // 2秒間無敵状態にする
        isInvincible = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            Debug.Log("床とプレイヤー");
            FloorScript flscript = collision.gameObject.GetComponent<FloorScript>();
            floorSpeed = flscript.floorSpeed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") //床から離れたら
        {
            floorSpeed = 0f;
        }
    }
}


