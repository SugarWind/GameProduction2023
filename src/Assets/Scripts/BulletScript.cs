using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject bulletA;
    public GameObject bulletD;
    public float bulletSpeed = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���N���b�N�Œe�𔭎�
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Z����0�ɌŒ�

            Vector3 shootDirection = (mousePosition - transform.position).normalized;
            GameObject Abullet = Instantiate(bulletA, transform.position, Quaternion.identity);
            Rigidbody2D rb = Abullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f; // �d�͉e���Ȃ�
            rb.velocity = shootDirection * bulletSpeed;
        }

        if (Input.GetMouseButtonDown(1)) // ���N���b�N�Œe�𔭎�
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Z����0�ɌŒ�

            Vector3 shootDirection = (mousePosition - transform.position).normalized;
            GameObject Dbullet = Instantiate(bulletD, transform.position, Quaternion.identity);
            Rigidbody2D rb = Dbullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f; // �d�͉e���Ȃ�
            rb.velocity = shootDirection * bulletSpeed;
        }
    }
}
