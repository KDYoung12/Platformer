using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;

    public float enemyHp;

    public float enemyMaxHp;

    public float enemyDamage;

    public GameObject Item;

    public Slider enemyHpBar;

    SpriteRenderer sprite;

    float move;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        enemyHp = enemyMaxHp;
    }

    private void Update()
    {
        move = enemySpeed * Time.deltaTime;
        transform.Translate(new Vector2(move, 0));
        if(move > 0)
        {
            sprite.flipX = false;
        }
        else if(move < 0)
        {
            sprite.flipX = true;
        }

        if(enemyHp <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(Item, gameObject.transform.position, Quaternion.identity);
        }
        enemyHpBar.value = enemyHp / enemyMaxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Turn"))
        {
            enemySpeed *= -1;
        }
    }

    public void HitColor()
    {
        StartCoroutine(HitBearColor());
    }

    IEnumerator HitBearColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }
}
