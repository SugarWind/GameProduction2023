using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveObject : MonoBehaviour
{
    [SerializeField] private Transform _objDestination;

    [SerializeField] private float _movePositionX = 12.0f;  //X軸の移動距離
    [SerializeField] private float _movePositionY = 0.0f;  //Y軸の移動距離
    [SerializeField] private float _moveSpeedX = 3.0f;  //X軸の速度
    [SerializeField] private float _moveSpeedY = 0.0f;  //Y軸の速度
    [SerializeField] private float _changeSpeed = 2;    //変化の倍率
    [SerializeField] private float _changeScale = 2;    //変化の段階

    private Rigidbody2D _rbObj;
    private float _defaultPositionX;
    private float _defaultPositionY;
    private float _targetPositionX;
    private float _targetPositionY;
    private float _defaultMoveSpeedX;
    private float _defaultMoveSpeedY;
    private float _maxSpeedX;    //X軸の最高速度
    private float _maxSpeedY;    //Y軸の最高速度
    private float _minSpeedX;    //X軸の最低速度
    private float _minSpeedY;    //Y軸の最低速度
    

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
