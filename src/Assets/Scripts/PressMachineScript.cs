using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMachineScript : MonoBehaviour
{
    [SerializeField] private Transform TopPos;
    Rigidbody2D rb;

    [SerializeField] private float MoveSpeed = 10.0f;
    [SerializeField] private float ChangeSpeed = 2.0f; //変化の倍率
    [SerializeField] private float ChangeScale = 1.0f; //変化の段階
    [SerializeField] private float WaitTime = 1.0f;

    private int direction = -1;
    private float _moveSpeed;
    private float _maxSpeed;    //最高速度
    private float _minSpeed;    //最低速度
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
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void FixedUpdate()
    {
        if (!_isStop)
        {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction));  
        }
        if (this.transform.position.y > TopPos.position.y && !_hasPressed)
        {
            _hasPressed = true;
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
            Debug.Log("プレス加速");
        }
        if (collision.gameObject.tag == "Dcammo" && MoveSpeed > _minSpeed && !_isHitDec)
        {
            _isHitDec = true;
            StartCoroutine(ChangeBoolCoroutine(_isHitDec, 0.1f));
            MoveSpeed /= ChangeSpeed;
            WaitTime *= ChangeSpeed;
            Debug.Log("プレス減速");
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

    private void ChangeSprite()    //加減速状態に応じてプレス機のスプライトを変更
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
        Debug.Log("プレス接触");
    }
}
