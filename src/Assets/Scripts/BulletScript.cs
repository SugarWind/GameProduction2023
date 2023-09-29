using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float shotInterval = 0.15f;
    [SerializeField] private GameObject accelBulletPrefab;
    [SerializeField] private GameObject deccelBulletPrefab;
    [SerializeField] private GameObject gunShaft;
    [SerializeField] private GameObject gunMuzzle;

    private GameObject playerObj;
    private PlayerScript player;

    private bool isHit = false;
    private bool canShot = true;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerScript>();
    }

    private void Update()
    {
        isHit = player.isHit;

        // �}�E�X�̈ʒu���擾
        Vector3 mousePosition = Input.mousePosition;

        // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // �e��������������v�Z
        Vector3 direction = mouseWorldPosition - gunShaft.transform.position;

        // �V���t�g���}�E�X�J�[�\���̈ʒu�ɍ��킹�Ď��]������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunShaft.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0) && canShot && !isHit) // ���N���b�N�Œe�i�����j�𔭎�
        {
            ShootCoolTime();
            ShotBullet(deccelBulletPrefab);
        }

        if (Input.GetMouseButtonDown(1) && canShot && !isHit) // ���N���b�N�Œe�i�����j�𔭎�
        {
            ShootCoolTime();
            ShotBullet(accelBulletPrefab);
        }
    }

    private void ShotBullet(GameObject bulletPrefab)
    {
        // mousePosition��mousePos�ɕύX 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Z����0�ɌŒ�

        // �e�̔��˕������v�Z
        Vector3 shootDirection = (mousePos - transform.position).normalized;

        // �}�Y���I�u�W�F�N�g����Z�b�g�����o���b�g�v���n�u�𔭎�
        GameObject bullet = Instantiate(bulletPrefab, gunMuzzle.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // �d�͉e���Ȃ�

        rb.velocity = shootDirection * bulletSpeed;
    }

    private IEnumerator ShootCoolTime()
    {
        canShot = false;
        yield return new WaitForSeconds(shotInterval);
        canShot = true;
    }
}
