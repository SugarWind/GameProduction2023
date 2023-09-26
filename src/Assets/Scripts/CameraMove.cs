using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
    // �Ǐ]�͈�
    [SerializeField] private float minPosX = -4f;
    [SerializeField] private float maxPosX = 49.2f;

    GameObject playerObj;
    PlayerScript player;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerScript.cs����v���C���[�̍��W���擾
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
        //�����������Ǐ]
        float playerPosX = Mathf.Clamp(playerTransform.position.x, minPosX, maxPosX);
        transform.position = new Vector3(playerPosX + 4, transform.position.y, transform.position.z);
    }
}
