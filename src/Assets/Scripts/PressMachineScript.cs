using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMachineScript : MonoBehaviour
{
    [SerializeField] private Transform TopPos;
    Rigidbody2D rb;

    [SerializeField] private float MoveSpeed = 3.0f;
    [SerializeField] private float ChangeSpeed = 2; //�ω��̔{��
    [SerializeField] private float ChangeScale = 2; //�ω��̒i�K

    int direction = -1;

    private float _moveSpeed;
    private float _maxSpeed;    //�ō����x
    private float _minSpeed;    //�Œᑬ�x
    private SpriteRenderer pressSprite;
    public Sprite PressDefault;
    public Sprite PressAcc;
    public Sprite PressDec;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pressSprite = gameObject.GetComponent<SpriteRenderer>();
        _moveSpeed = MoveSpeed;
        _maxSpeed = MoveSpeed * Mathf.Pow(ChangeSpeed, ChangeScale); 
        _minSpeed = MoveSpeed / Mathf.Pow(ChangeSpeed, ChangeScale); 
    }

    

    private void FixedUpdate()
    {

        rb.MovePosition(new Vector2(transform.position.x, transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction));

        if (transform.position.y > TopPos.position.y + 8)
        {
            direction *= -direction;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "floor")
        {
            direction = -direction;
            Debug.Log("�v���X�ڐG");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            MoveSpeed *= ChangeSpeed;
            Debug.Log("�v���X����");

            if(MoveSpeed > _maxSpeed)
            {
                MoveSpeed = _maxSpeed;
            }
        }
        if (collision.gameObject.tag == "Dcammo")
        {
            MoveSpeed /= ChangeSpeed;
            Debug.Log("�v���X����");

            if (MoveSpeed < _minSpeed)
            {
                MoveSpeed = _minSpeed;
            }
        }
        ChangePressSprite();
    }

    private void ChangePressSprite()    //��������Ԃɉ����ăv���X�@�̃X�v���C�g��ύX
    {
        if (MoveSpeed == _moveSpeed)
        {
            pressSprite.sprite = PressDefault;
        }
        else if (MoveSpeed > _moveSpeed)
        {
            pressSprite.sprite = PressAcc;
        }
        else if (MoveSpeed < _moveSpeed)
        {
            pressSprite.sprite = PressDec;
        }
    }
}
