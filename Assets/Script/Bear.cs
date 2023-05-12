using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public float bearSpeed;

    SpriteRenderer sprite;

    float move;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
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
