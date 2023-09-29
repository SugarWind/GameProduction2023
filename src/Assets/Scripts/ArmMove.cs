using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMove : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armSpriteRenderer;

    private GameObject playerObj;
    private PlayerScript player;
    private Transform playerTransform;

    private bool isHit = false;
    public bool Right = true;  // 腕の向きが更新したらPlayerScript.csで参照しキャラの向きを更新

    // Start is called before the first frame update
    void Start()
    {
        // PlayerScript.csからプレイヤーの座標を取得
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerScript>();
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // マウスの位置を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isHit = player.isHit;

        // 胴体の点滅と同時に腕を点滅
        armSpriteRenderer.enabled = player.spriteRenderer.enabled;

        // マウスのx座標に応じてオブジェクトの左右を反転
        if (mousePosition.x < playerTransform.position.x && !isHit)
        {
            // マウスが自機の左側にある場合、自機を左向きにします
            transform.localScale = new Vector3(1, -1, 1);
            Right = false;
        }
        else if (mousePosition.x >= playerTransform.position.x && !isHit)
        {
            // マウスが自機の右側にある場合、自機を右向きにします
            transform.localScale = new Vector3(1, 1, 1);
            Right = true;
        }
    }
}
