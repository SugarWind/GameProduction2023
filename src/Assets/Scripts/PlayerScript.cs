using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int jumpForce = 12;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float floorSpeed = 0f;
    [SerializeField] private float missileSpeed = 0f;
    [SerializeField] private float RmissileSpeed = 0f;
    [SerializeField] private float flashInterval = 0.04f;
    [SerializeField] private float knockbackForce = 200.0f;
    [SerializeField] private float invincibleInterval = 2.0f;
    [SerializeField] private Animator animator;
    [SerializeField] public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;
    private GameObject armObj;
    private ArmMove arm;
    private GameObject jumpObj;
    private JumpTrigger jump;
    private bool isJumping;
    private bool isFacingRight = true;  // キャラの向きを管理
    private bool hitDirection = true;  // 攻撃がきた方向を管理
    private bool isRidingMissile = false;
    private bool isRidingRmissile = false;
    private bool isRidingFloor = false;
    public bool isInvincible;
    public bool isHit;

    public AnimationClip jumpRightClip;  // 右向きジャンプアニメーション
    public AnimationClip jumpLeftClip;   // 左向きジャンプアニメーション
    public AnimationClip runRightClip;  // 右向き移動アニメーション
    public AnimationClip runLeftClip;   // 左向き移動アニメーション
    public AnimationClip standRightClip;  // 右向き停止アニメーション
    public AnimationClip standLeftClip;  // 左向き停止アニメーション
    public AnimationClip damageRightClip;  // 右から当たったアニメーション
    public AnimationClip damageLeftClip;  // 左から当たったアニメーション
    public AnimationClip backRightClip;  // 右向き後ろ歩きアニメーション
    public AnimationClip backLeftClip;  // 左向き後ろ歩きアニメーション

    public float moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        armObj = GameObject.FindGameObjectWithTag("Arm");
        arm = armObj.GetComponent<ArmMove>();
        jumpObj = GameObject.FindGameObjectWithTag("Jump");
        jump = jumpObj.GetComponent<JumpTrigger>();
        isHit = false;
        isJumping = false;
        isInvincible = false;

    }

    private void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");

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
        isJumping = jump.isJumping/* || rb.velocity.y != 0*/;

        // ArmRotation_yの左右反転に応じてキャラの向きを更新
        isFacingRight = arm.Right;

        // ジャンプアニメーションを設定
        if (isJumping && !isHit)
        {
            // isFacingRightがtrueならjumpRightClip.nameで指定したアニメーションを再生、falseならjumpLeftClip.name
            animator.Play(isFacingRight ? jumpRightClip.name : jumpLeftClip.name);
        }
        else if (!isJumping && !isHit)
        {
            // 移動アニメーションを設定
            if (moveDirection > 0)
            {
                animator.Play(isFacingRight ? runRightClip.name : backLeftClip.name);
                moveSpeed = (isFacingRight ? 4f : 3f);  // 前進は速く後進は遅く
            }
            else if (moveDirection < 0)
            {
                animator.Play(isFacingRight ? backRightClip.name : runLeftClip.name);
                moveSpeed = (isFacingRight ? 3f : 4f);
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
            animator.Play(isFacingRight ? damageLeftClip.name : damageRightClip.name);
        }
    }

    private void FixedUpdate()
    {
        /*float*/moveDirection = Input.GetAxis("Horizontal");

        if (!isRidingFloor && floorSpeed != 0)
        {
            if (!isJumping)
            {
                floorSpeed = 0f;  // 地面に着くまでは慣性が働く
            }
        }
        
        if (!isRidingMissile && missileSpeed != 0)
        {
            if (!isJumping)
            {
                missileSpeed = 0f;  // 地面に着くまでは慣性が働く
            }
        }

        if (!isRidingRmissile && RmissileSpeed != 0)
        {
            if (!isJumping)
            {
                RmissileSpeed = 0f;  // 地面に着くまでは慣性が働く
            }
        }


        if (!isHit)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed + floorSpeed + missileSpeed+RmissileSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

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
        if (collision.gameObject.tag == "floor" && !jump.isJumping)
        {
            Debug.Log("床とプレイヤー");
            FloorScript flscript = collision.gameObject.GetComponent<FloorScript>();
            floorSpeed = flscript.floorSpeed;
            isRidingFloor = true;
        }

        if (collision.gameObject.tag == "Missile")
        {
            MissileScript missile = collision.gameObject.GetComponent<MissileScript>();

            if(transform.position.y > missile.transform.position.y)  // 下からの接触判定を防止
            {
                missileSpeed = missile.Mspeed;
                isRidingMissile = true;
            }
        }

        if(collision.gameObject.tag =="Rmissile")
        {
            ReverseMissileScript Rmissile = collision.gameObject.GetComponent<ReverseMissileScript>();

            if (transform.position.y > Rmissile.transform.position.y)  
            {
                RmissileSpeed = Rmissile.Mspeed;
                isRidingRmissile = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") //床から離れたら
        {
            //isRidingFloor = false;

            if(moveDirection == 0)
            {
                floorSpeed = 0;
                isRidingFloor = false;
            }
            else
            {
                
                isRidingFloor = false;
            }
        }

        if (collision.gameObject.tag == "Missile")
        {
            //isRidingMissile = false;

            if (moveDirection >= 0)
            {
                missileSpeed = 0;
                isRidingMissile = false;

            }
            else
            {
                isRidingMissile = false;
            }
        }

        if(collision.gameObject.tag =="Rmissile")
        {
            if (moveDirection >= 0)
            {
                RmissileSpeed = 0;
                isRidingRmissile = false;

            }
            else
            {
                isRidingRmissile = false;
            }
        }
    }
}


