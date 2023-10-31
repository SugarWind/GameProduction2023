using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAutoMove : MonoBehaviour
{
    private Vector2 _objPosition;
    [SerializeField] private Vector2 _movingDistance;   //移動距離
    [SerializeField] private Vector2 _moveSpeed;        //速度
    [SerializeField] private const float _changeRate = 2;     //変化の倍率
    [SerializeField] private const uint _changeTimes = 2;    //変化の回数
    private Vector2 _movedPosition;     //移動後の位置
    private Vector2 _defaultPosition;   //デフォルトの位置
    private Vector2 _targetPosition;    //現在の目的地の位置
    private Vector2 _previousPosition;      //前回の目的地の位置
    private Vector2 _defaultMoveSpeed;  //デフォルトの速度
    private Vector2 _maxSpeed;  //最高速度
    private Vector2 _minSpeed;  //最低速度

    [SerializeField] private bool _needsDestroy;    //目的地についたときにgameObjectを破壊するか
    
    private bool _canMoveX, _canMoveY;    //現在移動可能か
    private bool _isGoingBack;  //戻っているのか
    private bool _spriteExists; //スプライトが存在するか
    private bool _animationExists;   //アニメーションが存在するか

    private SpriteRenderer _objSprite;
    [SerializeField] private Sprite _defaultSprite, _accSprite, _decSprite;  //ここにスプライトを入れる
    private Animator _objAnimator;
    [SerializeField] private AnimationClip _defaultAnimation, _accAnimation, _decAnimation;  //ここにアニメーションを入れる

    // Start is called before the first frame update
    private void Start()
    {
        _objPosition = transform.position;
        _defaultPosition = _objPosition;
        _movedPosition = new Vector2(_defaultPosition.x + _movingDistance.x, _defaultPosition.y + _movingDistance.y);
        _targetPosition = _movedPosition;
        _previousPosition = new Vector2(_defaultPosition.x, _defaultPosition.y);
        _defaultMoveSpeed = new Vector2(_moveSpeed.x, _moveSpeed.y);
        _maxSpeed = new Vector2(_moveSpeed.x * Mathf.Pow(_changeRate, _changeTimes), _moveSpeed.y * Mathf.Pow(_changeRate, _changeTimes));
        _minSpeed = new Vector2(_moveSpeed.x / Mathf.Pow(_changeRate, _changeTimes), _moveSpeed.y / Mathf.Pow(_changeRate, _changeTimes));

        _canMoveX = true;
        _canMoveY = true;
        _isGoingBack = false;
        if(_defaultAnimation && _accAnimation && _decAnimation)
        {
            _animationExists = true;
            _objAnimator = GetComponent<Animator>();
            _objAnimator.Play(_defaultAnimation.name);
        }
        else if (_defaultSprite && _accSprite && _decSprite)
        {
            _spriteExists = true;
            _objSprite = gameObject.GetComponent<SpriteRenderer>();
            _objSprite.sprite = _defaultSprite;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(_canMoveX)     //X方向に進めない場合実行しない
        {
            _objPosition.x += _moveSpeed.x * Time.deltaTime;   //X軸で移動
        }
        if(_canMoveY)     //Y方向に進めない場合実行しない
        {
            _objPosition.y += _moveSpeed.y * Time.deltaTime;   //Y軸で移動
        }

        if ((_previousPosition.x < _targetPosition.x && _objPosition.x > _targetPosition.x) || (_previousPosition.x > _targetPosition.x && _objPosition.x < _targetPosition.x) || _previousPosition.x == _targetPosition.x)
        {
            _objPosition.x = _targetPosition.x; //X方向に行き過ぎたときに戻す
            SetCanMove(false, _canMoveY);   //これ以上X方向に進めないようにする
        }
        if ((_previousPosition.y < _targetPosition.y && _objPosition.y > _targetPosition.y) || (_previousPosition.y > _targetPosition.y && _objPosition.y < _targetPosition.y) || _previousPosition.y == _targetPosition.y)
        {
            _objPosition.y = _targetPosition.y; //Y方向に行き過ぎたときに戻す
            SetCanMove(_canMoveX, false);   //これ以上Y方向に進めないようにする
        }

        //移動を反映
        this.transform.position = _objPosition;
        
        //目的地に着いたとき方向転換と目的地更新
        if (_canMoveX == false && _canMoveY == false)
        {
            if(_needsDestroy)
            {
                DestroyObject();    //"_needsDestroy"がtrueの時に破壊
            }
            _moveSpeed.x *= -1;
            _moveSpeed.y *= -1;
            SetCanMove(true, true);

            switch (_isGoingBack)
            {
                case true:
                    _targetPosition.x = _movedPosition.x;
                    _targetPosition.y = _movedPosition.y;
                    _previousPosition.x = _defaultPosition.x;
                    _previousPosition.y = _defaultPosition.y;
                    _isGoingBack = false;
                    break;
                case false:
                    _targetPosition.x = _defaultPosition.x;
                    _targetPosition.y = _defaultPosition.y;
                    _previousPosition.x = _movedPosition.x;
                    _previousPosition.y = _movedPosition.y;
                    _isGoingBack = true;
                    break;
            }
        }
    }

    public void SetCanMove(bool canMoveX, bool canMoveY)
    {
        _canMoveX = canMoveX;
        _canMoveY = canMoveY;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            _moveSpeed *= _changeRate;

            if(Mathf.Abs(_moveSpeed.x) > Mathf.Abs(_maxSpeed.x) && Mathf.Abs(_moveSpeed.y) > Mathf.Abs(_maxSpeed.y))
            {
                if(_isGoingBack)
                {
                    _moveSpeed = -_maxSpeed;
                }
                else 
                {
                    _moveSpeed = _maxSpeed;
                }
            }
        }
        if (collision.gameObject.tag == "Dcammo")
        {
            _moveSpeed /= _changeRate;

            if (Mathf.Abs(_moveSpeed.x) < Mathf.Abs(_minSpeed.x) && Mathf.Abs(_moveSpeed.y) < Mathf.Abs(_minSpeed.y))
            {
                if(_isGoingBack)
                {
                    _moveSpeed = -_minSpeed;
                }
                else 
                {
                    _moveSpeed = _minSpeed;
                }
            }
        }
        if (_spriteExists)
        {
            ChangeSprite();
        }
        if (_animationExists)
        {
            ChangeAnimation();
        }
    }

    private void ChangeSprite()    //加減速状態に応じてスプライトを変更
    {
        if (_moveSpeed == _defaultMoveSpeed || _moveSpeed == -_defaultMoveSpeed)
        {
            _objSprite.sprite = _defaultSprite;
        }
        else if (Mathf.Abs(_moveSpeed.x) > Mathf.Abs(_defaultMoveSpeed.x) && Mathf.Abs(_moveSpeed.y) > Mathf.Abs(_defaultMoveSpeed.y))
        {
            _objSprite.sprite = _accSprite;
        }
        else if (Mathf.Abs(_moveSpeed.x) < Mathf.Abs(_defaultMoveSpeed.x) && Mathf.Abs(_moveSpeed.y) < Mathf.Abs(_defaultMoveSpeed.y))
        {
            _objSprite.sprite = _decSprite;
        }
    }

    private void ChangeAnimation()    //加減速状態に応じてスプライトを変更
    {
        if (_moveSpeed == _defaultMoveSpeed || _moveSpeed == -_defaultMoveSpeed)
        {
            _objAnimator.Play(_defaultAnimation.name);
        }
        else if (Mathf.Abs(_moveSpeed.x) > Mathf.Abs(_defaultMoveSpeed.x) && Mathf.Abs(_moveSpeed.y) > Mathf.Abs(_defaultMoveSpeed.y))
        {
            _objAnimator.Play(_accAnimation.name);
        }
        else if (Mathf.Abs(_moveSpeed.x) < Mathf.Abs(_defaultMoveSpeed.x) && Mathf.Abs(_moveSpeed.y) < Mathf.Abs(_defaultMoveSpeed.y))
        {
            _objAnimator.Play(_decAnimation.name);
        }
    }
}
