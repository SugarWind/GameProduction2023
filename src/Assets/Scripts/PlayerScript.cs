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
    private bool isFacingRight = true;  // �L�����̌������Ǘ�
    private bool hitDirection = true;  // �U���������������Ǘ�
    private bool isRidingMissile = false;
    private bool isRidingRmissile = false;
    private bool isRidingFloor = false;
    public bool isInvincible;
    public bool isHit;

    private bool isposX;
    private bool isposY;

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

        isposX = PlayerPrefs.HasKey("PlayerPosX");
        isposY = PlayerPrefs.HasKey("PlayerPosY");

        if(!isposX)
        {
            Debug.Log("x���W�Ȃ�");
        }
        //Vector3 playerPosition = transform.position;

        if (isposX && isposY) //�`�F�b�N�|�C���g�̍��W���Z�[�u����Ă���
        {
            //���W�擾
            float PPosX = PlayerPrefs.GetFloat("PlayerPosX");
            float PPosY = PlayerPrefs.GetFloat("PlayerPosY");

            
            Vector2 SavePosition = new Vector2(PPosX, PPosY);

            //���W���v���C���[�ɃA�^�b�`
            transform.position = SavePosition;
        }
    }

    private void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");

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
        isJumping = jump.isJumping/* || rb.velocity.y != 0*/;

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
        /*float*/moveDirection = Input.GetAxis("Horizontal");

        if (!isRidingFloor && floorSpeed != 0)
        {
            if (!isJumping)
            {
                floorSpeed = 0f;  // �n�ʂɒ����܂ł͊���������
            }
        }
        
        if (!isRidingMissile && missileSpeed != 0)
        {
            if (!isJumping)
            {
                missileSpeed = 0f;  // �n�ʂɒ����܂ł͊���������
            }
        }

        if (!isRidingRmissile && RmissileSpeed != 0)
        {
            if (!isJumping)
            {
                RmissileSpeed = 0f;  // �n�ʂɒ����܂ł͊���������
            }
        }


        if (!isHit)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed + floorSpeed + missileSpeed+RmissileSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        // rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.velocity = new Vector2(0,1.25f) * jumpForce;
    }

    void SavePosition()
    {
        PlayerPrefs.SetFloat("PlayerPosX" ,transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY",transform.position.y);
    }

    void DeleteGoal()
    {
        PlayerPrefs.DeleteAll();
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
        if (collision.gameObject.tag == "floor" && !jump.isJumping)
        {
            Debug.Log("���ƃv���C���[");
            FloorScript flscript = collision.gameObject.GetComponent<FloorScript>();
            
            if(collision.gameObject.transform.rotation.y == 0)
            {
                floorSpeed = flscript.normal_floorSpeed;
            }
            else if (collision.gameObject.transform.rotation.y != 0)
            {
                floorSpeed = flscript.reverse_floorSpeed;
            }

            //floorSpeed = flscript.floorSpeed;
            isRidingFloor = true;
        }

        if (collision.gameObject.tag == "Missile")
        {
            MissileScript missile = collision.gameObject.GetComponent<MissileScript>();

            if(transform.position.y > missile.transform.position.y)  // ������̐ڐG�����h�~
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
        if (collision.gameObject.tag == "floor") //�����痣�ꂽ��
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


