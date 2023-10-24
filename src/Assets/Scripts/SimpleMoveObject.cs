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
    [SerializeField] private float _targetPositionX; //�ړ����X���W
    [SerializeField] private float _targetPositionY; //�ړ����Y���W
    [SerializeField] private float _fromPositionX;   //�ړ�����X���W
    [SerializeField] private float _fromPositionY;   //�ړ�����Y���W
    private float _defaultMoveSpeedX;
    private float _defaultMoveSpeedY;
    private float _maxSpeedX;    //X���̍ō����x
    private float _maxSpeedY;    //Y���̍ō����x
    private float _minSpeedX;    //X���̍Œᑬ�x
    private float _minSpeedY;    //Y���̍Œᑬ�x

    [SerializeField] private bool _hasReturned = false;

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
        _defaultPositionY = transform.position.y;
        _movePositionX = transform.position.x + _movePositionX;
        _movePositionY = transform.position.y + _movePositionY;
        _targetPositionX = _movePositionX;
        _targetPositionY = _movePositionY;
        _fromPositionX = _defaultPositionX;
        _fromPositionY = _defaultPositionY;
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

    private void FixedUpdate()
    {
        _rbObj.MovePosition(new Vector2(transform.position.x + _moveSpeedX * Time.fixedDeltaTime, transform.position.y + _moveSpeedY * Time.fixedDeltaTime));

        if ((_fromPositionX < _targetPositionX && transform.position.x > _targetPositionX)
            || (_fromPositionX > _targetPositionX && transform.position.x < _targetPositionX))
        {
            _rbObj.MovePosition(new Vector2(_targetPositionX, transform.position.y));
        }
        if ((_fromPositionY < _targetPositionY && transform.position.y > _targetPositionY)
            || (_fromPositionY > _targetPositionY && transform.position.y < _targetPositionY))
        {
            _rbObj.MovePosition(new Vector2(transform.position.x, _targetPositionY));
        }
        
        if (transform.position.x == _targetPositionX && transform.position.y == _targetPositionY)
        {
            _moveSpeedX = -_moveSpeedX;
            _moveSpeedY = -_moveSpeedY;
            switch (_hasReturned)
            {
                case true:
                    _targetPositionX = _defaultPositionX;
                    _targetPositionY = _defaultPositionY;
                    _fromPositionX = _movePositionX;
                    _fromPositionY = _movePositionY;
                    _rbObj.MovePosition(new Vector2(transform.position.x, _targetPositionY));
                    _hasReturned = false;
                    break;
                case false:
                    _targetPositionX = _movePositionX;
                    _targetPositionY = _movePositionY;
                    _fromPositionX = _defaultPositionX;
                    _fromPositionY = _defaultPositionY;
                    _hasReturned = true;
                    break;
            }
        }
    }
}
