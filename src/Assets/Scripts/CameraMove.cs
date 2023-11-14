using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
    // 追従範囲
    [SerializeField] private float minPosX = -4f;
    [SerializeField] private float maxPosX = 49.2f;

    [SerializeField] private float minPosY;
    [SerializeField] private float maxPosY;

    [SerializeField] private bool isVer = false;
    GameObject playerObj;
    PlayerScript player;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerScript.csからプレイヤーの座標を取得
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerScript>();
        playerTransform = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        //横方向だけ追従
        float playerPosX = Mathf.Clamp(playerTransform.position.x, minPosX, maxPosX);
        transform.position = new Vector3(playerPosX + 4, transform.position.y, transform.position.z);

        if (isVer)
        {
            float playerPosY = Mathf.Clamp(playerTransform.position.y, minPosY, maxPosY);
            transform.position = new Vector3(transform.position.x, playerPosY, transform.position.z);
        }
    }
}
