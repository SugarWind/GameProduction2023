using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveObject : MonoBehaviour
{
    private Vector2 _objPos;
    [SerializeField] private Vector2 _movePos;  //移動距離
    [SerializeField] private Vector2 _moveSpeed;    //速度
    [SerializeField] private float _changeSpeed = 2;    //変化の倍率
    [SerializeField] private float _changeScale = 2;    //変化の段階
    private Vector2 _defaultPos;    //デフォルトの座標
    private Vector2 _targetPos; //移動先の座標
    private Vector2 _fromPos;   //移動元の座標
    private Vector2 _defaultMoveSpeed;  //デフォルトの速度
    private Vector2 _maxSpeed;  //最高速度
    private Vector2 _minSpeed;  //最低速度

    [SerializeField] bool _canDestroy = false;
    [SerializeField] bool _hasSprite = false;
    [SerializeField] bool _hasAnim = false;
    private bool _canMovedX, _canMovedY;
    private bool _isReturn;


    private SpriteRenderer _objSprite;
    [SerializeField] private Sprite _objDefault, _objAcc, _objDec;

    // Start is called before the first frame update
    private void Start()
    {
        _objPos = transform.position;
        _defaultPos = _objPos;
        _movePos = new Vector2(_defaultPos.x + _movePos.x, _defaultPos.y + _movePos.y);
        _targetPos = _movePos;
        _fromPos = new Vector2(_defaultPos.x, _defaultPos.y);
        _defaultMoveSpeed = new Vector2(_moveSpeed.x, _moveSpeed.y);
        _maxSpeed = new Vector2(_moveSpeed.x * Mathf.Pow(_changeSpeed, _changeScale), _moveSpeed.y * Mathf.Pow(_changeSpeed, _changeScale));
        _minSpeed = new Vector2(_moveSpeed.x / Mathf.Pow(_changeSpeed, _changeScale),_moveSpeed.y / Mathf.Pow(_changeSpeed, _changeScale));

        _canMovedX = true;
        _canMovedY = true;
        _isReturn = false;

        _objSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(_canMovedX)
        {
            _objPos.x += _moveSpeed.x * Time.fixedDeltaTime; 
        }
        if(_canMovedY)
        {
            _objPos.y += _moveSpeed.y * Time.fixedDeltaTime;
        }

        if ((_fromPos.x < _targetPos.x && _objPos.x > _targetPos.x) || (_fromPos.x > _targetPos.x && _objPos.x < _targetPos.x) || _fromPos.x == _targetPos.x)
        {
            _objPos.x = _targetPos.x;
            CanMoveX(false);
        }
        if ((_fromPos.y < _targetPos.y && _objPos.y > _targetPos.y) || (_fromPos.y > _targetPos.y && _objPos.y < _targetPos.y) || _fromPos.y == _targetPos.y)
        {
            _objPos.y = _targetPos.y;
            CanMoveY(false);
        }

        this.transform.position = _objPos;
        
        if (_canMovedX == false && _canMovedY == false)
        {
            if(_canDestroy)
            {
                CanDestroy();
            }
            _moveSpeed.x *= -1;
            _moveSpeed.y *= -1;
            CanMoveX(true);
            CanMoveY(true);

            switch (_isReturn)
            {
                case true:
                    _targetPos.x = _movePos.x;
                    _targetPos.y = _movePos.y;
                    _fromPos.x = _defaultPos.x;
                    _fromPos.y = _defaultPos.y;
                    _isReturn = false;
                    break;
                case false:
                    _targetPos.x = _defaultPos.x;
                    _targetPos.y = _defaultPos.y;
                    _fromPos.x = _movePos.x;
                    _fromPos.y = _movePos.y;
                    _isReturn = true;
                    break;
            }
        }
    }

    public void CanMoveX(bool _canMX)
    {
        _canMovedX = _canMX;
    }

    public void CanMoveY(bool _canMY)
    {
        _canMovedY = _canMY;
    }

    public void CanDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            _moveSpeed *= _changeSpeed;

            if(_moveSpeed.x > _maxSpeed.x && _moveSpeed.y > _maxSpeed.y)
            {
                _moveSpeed = _maxSpeed;
            }
        }
        if (collision.gameObject.tag == "Dcammo")
        {
            _moveSpeed /= _changeSpeed;

            if (_moveSpeed.x < _minSpeed.x && _moveSpeed.y < _minSpeed.y)
            {
                _moveSpeed = _minSpeed;
            }
        }
        if(_hasSprite){
            ChangePressSprite();
        }
    }

    private void ChangePressSprite()    //加減速状態に応じてプレス機のスプライトを変更
    {
        if (_moveSpeed == _defaultMoveSpeed)
        {
            _objSprite.sprite = _objDefault;
        }
        else if (_moveSpeed.x > _defaultMoveSpeed.x && _moveSpeed.y > _defaultMoveSpeed.y)
        {
            _objSprite.sprite = _objAcc;
        }
        else if (_moveSpeed.x < _defaultMoveSpeed.x && _moveSpeed.y < _defaultMoveSpeed.y)
        {
            _objSprite.sprite = _objDec;
        }
    }
}
