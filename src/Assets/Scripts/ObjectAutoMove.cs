using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAutoMove : MonoBehaviour
{
    private Vector2 _objPosition;
    [SerializeField] private Vector2 _movingDistance;   //�ړ�����
    [SerializeField] private Vector2 _moveSpeed;        //���x
    [SerializeField] private float _changeRate = 2;     //�ω��̔{��
    [SerializeField] private float _changeTimes = 2;    //�ω��̉�
    private Vector2 _movedPosition;     //�ړ���̈ʒu
    private Vector2 _defaultPosition;   //�f�t�H���g�̈ʒu
    private Vector2 _targetPosition;    //���݂̖ړI�n�̈ʒu
    private Vector2 _previousPosition;      //�O��̖ړI�n�̈ʒu
    private Vector2 _defaultMoveSpeed;  //�f�t�H���g�̑��x
    private Vector2 _maxSpeed;  //�ō����x
    private Vector2 _minSpeed;  //�Œᑬ�x

    [SerializeField] private bool _needsDestroy = false;    //�ړI�n�ɂ����Ƃ���gameObject��j�󂷂邩
    
    private bool _canMoveX, _canMoveY;    //���݈ړ��\��
    private bool _isGoingBack;  //�߂��Ă���̂�
    private bool _spriteExists; //�X�v���C�g�����݂��邩
    private bool _animationExists;   //�A�j���[�V���������݂��邩

    private SpriteRenderer _objSprite;
    [SerializeField] private Sprite _defaultSprite, _accSprite, _decSprite;  //�����ɃX�v���C�g������

    [SerializeField] private AnimationClip _defaultAnimation, _accAnimation, _decAnimation;  //�����ɃA�j���[�V����������

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
        if(_defaultSprite && _accSprite && _decSprite)
        {
            _spriteExists = true;
            _objSprite = gameObject.GetComponent<SpriteRenderer>();
            _objSprite.sprite = _defaultSprite;
        }
        if(_defaultAnimation && _accAnimation && _decAnimation)
        {
            _animationExists = true;
            _spriteExists = false;
        }

        _objSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(_canMoveX)     //X�����ɐi�߂Ȃ��ꍇ���s���Ȃ�
        {
            _objPosition.x += _moveSpeed.x * Time.deltaTime;   //X���ňړ�
        }
        if(_canMoveY)     //Y�����ɐi�߂Ȃ��ꍇ���s���Ȃ�
        {
            _objPosition.y += _moveSpeed.y * Time.deltaTime;   //Y���ňړ�
        }

        if ((_previousPosition.x < _targetPosition.x && _objPosition.x > _targetPosition.x) || (_previousPosition.x > _targetPosition.x && _objPosition.x < _targetPosition.x) || _previousPosition.x == _targetPosition.x)
        {
            _objPosition.x = _targetPosition.x; //X�����ɍs���߂����Ƃ��ɖ߂�
            SetCanMove(false, _canMoveY);   //����ȏ�X�����ɐi�߂Ȃ��悤�ɂ���
        }
        if ((_previousPosition.y < _targetPosition.y && _objPosition.y > _targetPosition.y) || (_previousPosition.y > _targetPosition.y && _objPosition.y < _targetPosition.y) || _previousPosition.y == _targetPosition.y)
        {
            _objPosition.y = _targetPosition.y; //Y�����ɍs���߂����Ƃ��ɖ߂�
            SetCanMove(_canMoveX, false);   //����ȏ�Y�����ɐi�߂Ȃ��悤�ɂ���
        }

        //�ړ��𔽉f
        this.transform.position = _objPosition;
        
        //�ړI�n�ɒ������Ƃ������]���ƖړI�n�X�V
        if (_canMoveX == false && _canMoveY == false)
        {
            if(_needsDestroy)
            {
                DestroyObject();    //"_needsDestroy"��true�̎��ɔj��
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
        if (_spriteExists){
            ChangeSprite();
        }
    }

    private void ChangeSprite()    //��������Ԃɉ����ăX�v���C�g��ύX
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
}