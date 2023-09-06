using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject bulletA;
    public GameObject bulletD;
    public float bulletSpeed = 10f;

    private bool isLeft;
    private bool isRight;
    private float ShootTime = 0f;

    private void Update()
    {
        ShootTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0)&& isRight == false && ShootTime >0.15f) // 左クリックで弾を発射
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Z軸を0に固定

            Vector3 shootDirection = (mousePosition - transform.position).normalized;
            GameObject Abullet = Instantiate(bulletA, transform.position, Quaternion.identity);
            Rigidbody2D rb = Abullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f; // 重力影響なし
            rb.velocity = shootDirection * bulletSpeed;

            isLeft = true;
            ShootTime = 0f;
        }
        else
        {
            isLeft = false;
        }

        if (Input.GetMouseButtonDown(1)&& isLeft == false && ShootTime > 0.15f) // 左クリックで弾を発射
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Z軸を0に固定

            Vector3 shootDirection = (mousePosition - transform.position).normalized;
            GameObject Dbullet = Instantiate(bulletD, transform.position, Quaternion.identity);
            Rigidbody2D rb = Dbullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f; // 重力影響なし
            rb.velocity = shootDirection * bulletSpeed;
            isRight = true;
            ShootTime = 0f;
        }
        else
        {
            isRight = false;
        }
    }
}
