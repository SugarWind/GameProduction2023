using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMachineScript : MonoBehaviour
{
    [SerializeField] private Transform TopPos;
    Rigidbody2D rb;

    [SerializeField] private float MoveSpeed = 3.0f;
    [SerializeField] private float ChangeSpeed = 2.0f; //変化の倍率
    [SerializeField] private float ChangeScale = 2.0f; //変化の段階
    [SerializeField] private float WaitTime = 1.0f;

    private int direction = -1;

    private float _moveSpeed;
    private float _maxSpeed;    //最高速度
    private float _minSpeed;    //最低速度
    private SpriteRenderer pressSprite;
    private bool _isStop;
    [SerializeField] private Sprite PressDefault;
    [SerializeField] private Sprite PressAcc;
    [SerializeField] private Sprite PressDec;
    [SerializeField] private AudioClip _pressMachineSound;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
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

            if (transform.position.y > TopPos.position.y + 8)
            {
                direction *= -direction;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "floor")
        {
            _isStop = true;
            Invoke("ReturnPress", WaitTime);
            Debug.Log("プレス接触");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo" && MoveSpeed < _maxSpeed)
        {
            MoveSpeed *= ChangeSpeed;
            WaitTime /= ChangeSpeed;
            Debug.Log("プレス加速");
        }
        if (collision.gameObject.tag == "Dcammo" && MoveSpeed > _minSpeed)
        {
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
}
