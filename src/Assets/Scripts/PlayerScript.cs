using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int jumpForce = 12;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float floorSpeed = 0f;
    [SerializeField] private float missileSpeed = 0f;
    [SerializeField] private float otherSpeed = 0f;  // �L�[����ɂ��ړ��ȊO�ɉ����X�s�[�h
    [SerializeField] private float flashInterval = 0.04f;
    [SerializeField] private float knockbackForce = 200.0f;
    [SerializeField] private float invincibleInterval = 2.0f;
    [SerializeField] private Animator animator;
    [SerializeField] public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private GameObject armObj;
    private ArmMove arm;
    private bool isJumping;
    private bool isFacingRight = true;  // �L�����̌������Ǘ�
    private bool hitDirection = true;  // �U���������������Ǘ�
    private bool isRidingMissile = false;
    private bool isRidingFloor = false;
    public bool isInvincible;
    public bool isHit;

    public AnimationClip jumpRightClip;  // �E�����W�����v�A�j���[�V����
    public AnimationClip jumpLeftClip;   // �������W�����v�A�j���[�V����
    public AnimationClip runRightClip;  // �E�����ړ��A�j���[�V����
    public AnimationClip runLeftClip;   // �������ړ��A�j���[�V����
    public AnimationClip standRightClip;  // �E������~�A�j���[�V����
    public AnimationClip standLeftClip;  // ��������~�A�j���[�V����
    public AnimationClip damageRightClip;  // �E���瓖�������A�j���[�V����
    public AnimationClip damageLeftClip;  // �����瓖�������A�j���[�V����
    public AnimationClip backRightClip;  // �E�����������A�j���[�V����
    public AnimationClip backLeftClip;  // �������������A�j���[�V����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        armObj = GameObject.FindGameObjectWithTag("Arm");
        arm = armObj.GetComponent<ArmMove>();
        isHit = false;
        isJumping = false;
        isInvincible = false;

    }

    private void Update()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        // �W�����v    
        if (Input.GetKeyDown(KeyCode.W) && !isJumping && !isHit)
        {
            Jump();
        }

        // �A�j���[�V�����ݒ�
        UpdateAnimations(moveDirection);
    }

    private void UpdateAnimations(float moveDirection)
    {
        // �W�����v�������X�V
        // �L�����̐��������̑��x��0�łȂ��ꍇ�Atrue
        isJumping = rb.velocity.y != 0;

        // ArmRotation_y�̍��E���]�ɉ����ăL�����̌������X�V
        isFacingRight = arm.Right;

        // �W�����v�A�j���[�V������ݒ�
        if (isJumping && !isHit)
        {
            // isFacingRight��true�Ȃ�jumpRightClip.name�Ŏw�肵���A�j���[�V�������Đ��Afalse�Ȃ�jumpLeftClip.name
            animator.Play(isFacingRight ? jumpRightClip.name : jumpLeftClip.name);
        }
        else if (!isJumping && !isHit)
        {
            // �ړ��A�j���[�V������ݒ�
            if (moveDirection > 0)
            {
                animator.Play(isFacingRight ? runRightClip.name : backLeftClip.name);
                moveSpeed = (isFacingRight ? 4f : 3f);  // �O�i�͑�����i�͒x��
            }
            else if (moveDirection < 0)
            {
                animator.Play(isFacingRight ? backRightClip.name : runLeftClip.name);
                moveSpeed = (isFacingRight ? 3f : 4f);
            }
            // ��~�A�j���[�V������ݒ�
            else
            {
                animator.Play(isFacingRight ? standRightClip.name : standLeftClip.name);
            }
        }
        // �_���[�W�A�j���[�V������ݒ�
        else if (isHit)
        {
            animator.Play(isFacingRight ? damageLeftClip.name : damageRightClip.name);
        }
    }

    private void FixedUpdate()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        if (!isRidingFloor)
        {
            if (rb.velocity.y == 0)
            {
                floorSpeed = 0f;  // �n�ʂɒ����܂ł͊���������
            }
        }

        if (!isHit)
        {
            otherSpeed = (isRidingMissile ? missileSpeed : floorSpeed);
            rb.velocity = new Vector2(moveDirection * moveSpeed + otherSpeed, rb.velocity.y);
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
            // �m�b�N�o�b�N����������̌v�Z
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            Knockback(knockbackDirection);  // �m�b�N�o�b�N������
            StartCoroutine(Flash());  // �_�ł�����
            StartCoroutine(Invincible());  // ���G��Ԃɂ���
        }
    }

    private void Knockback(Vector2 direction)
    {
        // direction�x�N�g����x�����Ȃ�false�A���Ȃ�true
        hitDirection = direction.x < 0;

        rb.AddForce(transform.right * (hitDirection ? -knockbackForce : knockbackForce));
    }

    private IEnumerator Flash()
    {
        // isHit��true�̊Ԃ͓����Ȃ�
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
        yield return new WaitForSeconds(invincibleInterval); // 2�b�Ԗ��G��Ԃɂ���
        isInvincible = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            Debug.Log("���ƃv���C���[");
            FloorScript flscript = collision.gameObject.GetComponent<FloorScript>();
            floorSpeed = flscript.floorSpeed;
            isRidingFloor = true;
        }

        if(collision.gameObject.tag == "Missile")
        {
            MissileScript missile = collision.gameObject.GetComponent<MissileScript>();

            if(transform.position.y > missile.transform.position.y)  // ������̐ڐG�����h�~
            {
                missileSpeed = missile.Mspeed;
                isRidingMissile = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") //�����痣�ꂽ��
        {
            isRidingFloor = false;
        }

        if (collision.gameObject.tag == "Missile")
        {
            isRidingMissile = false;
        }
    }
}


