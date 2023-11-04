using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMachineScript : MonoBehaviour
{
    [SerializeField] private Transform TopPos;
    Rigidbody2D rb;

    [SerializeField] private float MoveSpeed = 3.0f;
    [SerializeField] private float ChangeSpeed = 2; //変化の倍率
    [SerializeField] private float ChangeScale = 2; //変化の段階

    private int direction = -1;

    private float _moveSpeed;
    private float _maxSpeed;    //最高速度
    private float _minSpeed;    //最低速度
    private SpriteRenderer pressSprite;
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
            _audioSource.PlayOneShot(_pressMachineSound);
            direction = -direction;
            Debug.Log("プレス接触");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            MoveSpeed *= ChangeSpeed;
            Debug.Log("プレス加速");

            if(MoveSpeed > _maxSpeed)
            {
                MoveSpeed = _maxSpeed;
            }
        }
        if (collision.gameObject.tag == "Dcammo")
        {
            MoveSpeed /= ChangeSpeed;
            Debug.Log("プレス減速");

            if (MoveSpeed < _minSpeed)
            {
                MoveSpeed = _minSpeed;
            }
        }
        ChangeSprite();
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
