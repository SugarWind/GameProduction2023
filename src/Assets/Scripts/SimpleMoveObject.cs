using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveObject : MonoBehaviour
{
    [SerializeField] private Transform _objDestination;

    [SerializeField] private float _movePositionX = 12.0f;  //X���̈ړ�����
    [SerializeField] private float _movePositionY = 0.0f;  //Y���̈ړ�����
    [SerializeField] private float _moveSpeedX = 3.0f;  //X���̑��x
    [SerializeField] private float _moveSpeedY = 0.0f;  //Y���̑��x
    [SerializeField] private float _changeSpeed = 2;    //�ω��̔{��
    [SerializeField] private float _changeScale = 2;    //�ω��̒i�K

    private Rigidbody2D _rbObj;
    private float _defaultPositionX;
    private float _defaultPositionY;
    private float _targetPositionX;
    private float _targetPositionY;
    private float _defaultMoveSpeedX;
    private float _defaultMoveSpeedY;
    private float _maxSpeedX;    //X���̍ō����x
    private float _maxSpeedY;    //Y���̍ō����x
    private float _minSpeedX;    //X���̍Œᑬ�x
    private float _minSpeedY;    //Y���̍Œᑬ�x
    

    private SpriteRenderer _objSprite;
    [SerializeField] private Sprite _objDefault;
    [SerializeField] private Sprite _objAcc;
    [SerializeField] private Sprite _objDec;

    // Start is called before the first frame update
    void Start()
    {
        _rbObj = GetComponent<Rigidbody2D>();
        _objSprite = gameObject.GetComponent<SpriteRenderer>();
        _defaultPositionX = transform.position.x;
        _defaultPositionX = transform.position.y;
        _targetPositionX = transform.position.x + _movePositionX;
        _targetPositionY = transform.position.y + _movePositionY;
        _defaultMoveSpeedX = _moveSpeedX;
        _defaultMoveSpeedY = _moveSpeedY;
        _maxSpeedX = _moveSpeedX * Mathf.Pow(_changeSpeed, _changeScale);
        _maxSpeedY = _moveSpeedY * Mathf.Pow(_changeSpeed, _changeScale);
        _minSpeedX = _moveSpeedX / Mathf.Pow(_changeSpeed, _changeScale);
        _minSpeedY = _moveSpeedY / Mathf.Pow(_changeSpeed, _changeScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
