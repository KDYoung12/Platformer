using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed;

    public float jumpPower;

    public float damage;

    public Collider2D groundColl;

    Animator anim;
    Rigidbody2D rigid2D;
    SpriteRenderer sprite;

    int _jumpCnt;
    int _maxJumpCount;

    bool isJump;
    bool isWalk;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _jumpCnt = 0;
        _maxJumpCount = 1;
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        if (isWalk)
        {
            transform.Translate(new Vector2(h, 0));
            anim.SetBool("isRun", true);
        }
        if (h < 0)
        {
            sprite.flipX = true;
        }
        else if (h > 0)
        {
            sprite.flipX = false;
        }
        else if (h == 0)
        {
            anim.SetBool("isRun", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCnt < _maxJumpCount && isJump)
        {
            rigid2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            _jumpCnt++;
            Debug.Log(_jumpCnt);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = true;
            isWalk = true;
            _jumpCnt = 0;
            anim.SetBool("isJump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            GameManager.instance.keyCount++;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("JumpItem"))
        {
            jumpPower += 4f;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy.instance.enemyHp -= this.damage;
            Enemy.instance.HitColor();
            int randint = Random.Range(-3, 4);
            Debug.Log(randint);
            rigid2D.AddForce(new Vector2(randint, 20f), ForceMode2D.Impulse);
            Debug.Log(Enemy.instance.enemyHp);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder") && Input.GetKey(KeyCode.W))
        {
            isJump = false;
            isWalk = false;
            _jumpCnt = 99;
            rigid2D.gravityScale = 0;
            groundColl.usedByEffector = true;
            transform.Translate(new Vector2(0, 0.05f));
            anim.SetTrigger("onLadder");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            _jumpCnt = 0;
            rigid2D.gravityScale = 1.5f;
            groundColl.usedByEffector = false;
        }
    }
}
