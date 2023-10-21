using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveObject : MonoBehaviour
{
    [SerializeField] private Transform _objDestination;

    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _changeSpeed = 2; //変化の倍率
    [SerializeField] private float _changeScale = 2; //変化の段階

    private Rigidbody2D _rbObj;
    private float _moveSpeed;
    private float _maxSpeed;    //最高速度
    private float _minSpeed;    //最低速度
    private SpriteRenderer _objSprite;
    public Sprite _objDefault;
    public Sprite _objAcc;
    public Sprite _objDec;

    // Start is called before the first frame update
    void Start()
    {
        _rbObj = GetComponent<Rigidbody2D>();
        pressSprite = gameObject.GetComponent<SpriteRenderer>();
        _moveSpeed = MoveSpeed;
        _maxSpeed = MoveSpeed * Mathf.Pow(ChangeSpeed, ChangeScale);
        _minSpeed = MoveSpeed / Mathf.Pow(ChangeSpeed, ChangeScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
