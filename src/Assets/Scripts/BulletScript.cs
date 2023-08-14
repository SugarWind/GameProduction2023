using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject bulletA;
    public float bulletSpeed = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリックで弾を発射
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Z軸を0に固定

            Vector3 shootDirection = (mousePosition - transform.position).normalized;
            GameObject bullet = Instantiate(bulletA, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f; // 重力影響なし
            rb.velocity = shootDirection * bulletSpeed;
        }
    }
}
