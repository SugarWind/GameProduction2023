using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondArmScript : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _rightSprite;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSpriteLeft()
    {
        _spriteRenderer.sprite = _leftSprite;
    }
    public void ChangeSpriteRight()
    {
        _spriteRenderer.sprite = _rightSprite;
    }
}
