using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public enum Enemyes { pig, slime, bear }

public class Enemy : MonoBehaviour
{
    public float enemySpeed;

    public float enemyHp;

    public float enemyMaxHp;

    public float enemyDamage;

    public GameObject Item;

    public Slider enemyHpBar;

    Player player;

    SpriteRenderer sprite;

    float move;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
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
            //player.gold += 10;
            gameObject.SetActive(false);
            if(Item != null)
            {
                Instantiate(Item, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f) , Quaternion.identity);
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().playerHp -= (Mathf.Abs(collision.gameObject.GetComponent<Player>().defense) - Mathf.Abs(enemyDamage));
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
