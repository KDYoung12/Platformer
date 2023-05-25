using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    public float bulletDamage;

    float lifeTime = 1f;
    void Update()
    {
        lifeTime -= Time.deltaTime;

        transform.Translate(new Vector2(bulletSpeed * Time.deltaTime, 0f));
        
        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().enemyHp -= bulletDamage;
            collision.GetComponent<Enemy>().HitColor();
            Destroy(this.gameObject);
        }
    }
}
