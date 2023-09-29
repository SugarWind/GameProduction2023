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
    public bool Right = true;  // �r�̌������X�V������PlayerScript.cs�ŎQ�Ƃ��L�����̌������X�V

    // Start is called before the first frame update
    void Start()
    {
        // PlayerScript.cs����v���C���[�̍��W���擾
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerScript>();
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X�̈ʒu���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isHit = player.isHit;

        // ���̂̓_�łƓ����ɘr��_��
        armSpriteRenderer.enabled = player.spriteRenderer.enabled;

        // �}�E�X��x���W�ɉ����ăI�u�W�F�N�g�̍��E�𔽓]
        if (mousePosition.x < playerTransform.position.x && !isHit)
        {
            // �}�E�X�����@�̍����ɂ���ꍇ�A���@���������ɂ��܂�
            transform.localScale = new Vector3(1, -1, 1);
            Right = false;
        }
        else if (mousePosition.x >= playerTransform.position.x && !isHit)
        {
            // �}�E�X�����@�̉E���ɂ���ꍇ�A���@���E�����ɂ��܂�
            transform.localScale = new Vector3(1, 1, 1);
            Right = true;
        }
    }
}
