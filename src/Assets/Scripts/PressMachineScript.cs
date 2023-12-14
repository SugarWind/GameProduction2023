using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMachineScript : MonoBehaviour
{
    [SerializeField] private Transform TopPos;
    Rigidbody2D rb;

    [SerializeField] private float MoveSpeed = 10.0f;
    [SerializeField] private float ChangeSpeed = 2.0f; //�ω��̔{��
    [SerializeField] private float ChangeScale = 1.0f; //�ω��̒i�K
    [SerializeField] private float WaitTime = 1.0f;

    private int direction = -1;
    private float _moveSpeed;
    private float _maxSpeed;    //�ō����x
    private float _minSpeed;    //�Œᑬ�x
    private float _underPositionY;
    private SpriteRenderer pressSprite;
    private bool _isStop;
    private bool _isHitAcc;
    private bool _isHitDec;
    private bool _hasPressed;
    [SerializeField] private Sprite PressDefault;
    [SerializeField] private Sprite PressAcc;
    [SerializeField] private Sprite PressDec;
    [SerializeField] private AudioClip _pressMachineSound;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if(!TopPos) TopPos = this.transform;
        rb = GetComponent<Rigidbody2D>();
        pressSprite = gameObject.GetComponent<SpriteRenderer>();
        _moveSpeed = MoveSpeed;
        _maxSpeed = MoveSpeed * Mathf.Pow(ChangeSpeed, ChangeScale);
        _minSpeed = MoveSpeed / Mathf.Pow(ChangeSpeed, ChangeScale);
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!_isStop)
        {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction));  
        }
        if (this.transform.position.y > TopPos.position.y)
        {
            StartCoroutine(ChangeBoolCoroutine(_hasPressed, 0.1f));
            this.transform.position = new Vector2(this.transform.position.x, TopPos.position.y);
            direction = -direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo" && MoveSpeed < _maxSpeed && !_isHitAcc)
        {
            _isHitAcc = true;
            StartCoroutine(ChangeBoolCoroutine(_isHitAcc, 0.1f));
            MoveSpeed *= ChangeSpeed;
            WaitTime /= ChangeSpeed;
            Debug.Log("�v���X����");
        }
        if (collision.gameObject.tag == "Dcammo" && MoveSpeed > _minSpeed && !_isHitDec)
        {
            _isHitDec = true;
            StartCoroutine(ChangeBoolCoroutine(_isHitDec, 0.1f));
            MoveSpeed /= ChangeSpeed;
            WaitTime *= ChangeSpeed;
            Debug.Log("�v���X����");
        }
        ChangeSprite();
    }

    private void ReturnPress()
    {
        _audioSource.PlayOneShot(_pressMachineSound);
        direction = -direction;
        _isStop = false;
    }

    public IEnumerator ChangeBoolCoroutine(bool _isBool, float num)
    {
        _isBool = false;
        yield return new WaitForSeconds(num);
    }

    private void ChangeSprite()    //��������Ԃɉ����ăv���X�@�̃X�v���C�g��ύX
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

    public void OnCollisionReturn()
    {
        if (!_hasPressed)
        {
            _hasPressed = true;
            _underPositionY = Mathf.Round(this.transform.position.y);
        }
        if (this.transform.position.y < _underPositionY)
        {
            this.transform.position = new Vector2(this.transform.position.x, _underPositionY);
        }
        _isStop = true;
        Invoke("ReturnPress", WaitTime);
        Debug.Log("�v���X�ڐG");
    }
}
