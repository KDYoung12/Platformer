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

    bool isLadder;
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
        if (!isLadder)
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

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCnt < _maxJumpCount && !isLadder)
        {
            rigid2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            _jumpCnt++;
            Debug.Log(_jumpCnt);
        }

        if (Input.GetKey(KeyCode.W) && isLadder)
        {
            Debug.Log("올라가는 중");
            transform.Translate(new Vector2(0, 0.0005f));
            anim.SetTrigger("onLadder");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 6)
        {
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
            jumpPower += 2f;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().enemyHp -= this.damage;
            collision.GetComponent<Enemy>().HitColor();
            //Enemy.instance.enemyHp -= this.damage;
            //Enemy.instance.HitColor();
            int randintX = Random.Range(-3, 4);
            int randintY = Random.Range(10, 15);
            Debug.Log(randintX + "이건 X");
            Debug.Log(randintY + "이건 Y");
            rigid2D.AddForce(new Vector2(randintX, randintY), ForceMode2D.Impulse);
            Debug.Log(collision.GetComponent<Enemy>().enemyHp);
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
            // 지금 사다리 타고있는 중이라면 isLadder = false
            rigid2D.gravityScale = 0;
            groundColl.usedByEffector = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            _jumpCnt = 0;
            rigid2D.gravityScale = 1.5f;
            groundColl.usedByEffector = false;
        }
    }
}
