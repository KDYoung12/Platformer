using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;

    public float enemySpeed;

    public float enemyHp;

    public float enemyMaxHp;

    public float enemyDamage;

    public GameObject key;

    SpriteRenderer sprite;

    float move;

    private void Awake()
    {
        instance = this;
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
            Instantiate(key, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Turn"))
        {
            Debug.Log("Tile end");
            enemySpeed *= -1;
            Debug.Log(move.ToString());
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
