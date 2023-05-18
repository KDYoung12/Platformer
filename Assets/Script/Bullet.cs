using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    public float bulletDamage;

    void Update()
    {
        transform.Translate(new Vector2(bulletSpeed * Time.deltaTime, 0f));        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().enemyHp -= bulletDamage;
            collision.GetComponent<Enemy>().HitColor();
            gameObject.SetActive(false);
        }
    }
}
