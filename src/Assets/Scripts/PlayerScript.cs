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
    private bool isFacingRight = true;  // �L�����̌������Ǘ�
    private bool hitDirection = true;  // �U���������������Ǘ�

    public AnimationClip jumpRightClip;  // �E�����W�����v�A�j���[�V����
    public AnimationClip jumpLeftClip;   // �������W�����v�A�j���[�V����
    public AnimationClip runRightClip;  // �E�����ړ��A�j���[�V����
    public AnimationClip runLeftClip;   // �������ړ��A�j���[�V����
    public AnimationClip standRightClip;  // �E������~�A�j���[�V����
    public AnimationClip standLeftClip;  // ��������~�A�j���[�V����
    public AnimationClip damageRightClip;  // �E���瓖�������A�j���[�V����
    public AnimationClip damageLeftClip;  // �����瓖�������A�j���[�V����

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

        // �L�����̌������X�V
        if (moveDirection > 0.1f && !isJumping)
        {
            isFacingRight = true;
        }
        else if (moveDirection < -0.1f && !isJumping)
        {
            isFacingRight = false;
        }


        // �W�����v�A�j���[�V������ݒ�
        if (isJumping && !isHit)
        {
            // isFacingRight��true�Ȃ�jumpRightClip.name�Ŏw�肵���A�j���[�V�������Đ��Afalse�Ȃ�jumpLeftClip.name
            animator.Play(isFacingRight ? jumpRightClip.name : jumpLeftClip.name);
        }
        else if (!isJumping && !isHit)
        {
            // �ړ��A�j���[�V������ݒ�
            if (moveDirection != 0)
            {
                animator.Play(isFacingRight ? runRightClip.name : runLeftClip.name);
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
            // �U�����E���痈���Ȃ�L�����̌������E�����ɁA������Ȃ獶������
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

    // �G�ƂԂ�������
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") //�����痣�ꂽ��
        {
            floorSpeed = 0f;
        }
    }
}


