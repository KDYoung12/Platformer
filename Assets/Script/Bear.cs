using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public static Bear instance;

    public float bearSpeed;

    public float bearHp;

    public float bearMaxHp;

    public float bearDamage;

    public GameObject key;

    SpriteRenderer sprite;

    float move;

    private void Awake()
    {
        instance = this;
        sprite = GetComponent<SpriteRenderer>();
        bearHp = bearMaxHp;
    }

    private void Update()
    {
        move = bearSpeed * Time.deltaTime;
        transform.Translate(new Vector2(move, 0));
        if(move > 0)
        {
            sprite.flipX = false;
        }
        else if(move < 0)
        {
            sprite.flipX = true;
        }

        if(bearHp <= 0)
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
            bearSpeed *= -1;
            Debug.Log(move.ToString());
        }
    }
}
