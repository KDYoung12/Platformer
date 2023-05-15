using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    public float jumpPower;

    public float damage;

    public Collider2D groundColl;

    Animator anim;
    Rigidbody2D rigid2D;
    SpriteRenderer sprite;

    int _jumpCnt;
    int _maxJumpCount;
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
        float h = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        transform.Translate(new Vector2(h, 0));

        if (h < 0)
        {
            anim.SetBool("isRun", true);
            sprite.flipX = true;
        }
        else if (h > 0)
        {
            anim.SetBool("isRun", true);
            sprite.flipX = false;
        }
        else if (h == 0)
        {
            anim.SetBool("isRun", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCnt < _maxJumpCount)
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
            _jumpCnt = 0;
            anim.SetBool("isJump", false);
        }

        if (collision.gameObject.CompareTag("Bear"))
        {
            Bear.instance.bearHp -= this.damage;
            Debug.Log(Bear.instance.bearHp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            GameManager.instance.keyCount++;
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder") && Input.GetKey(KeyCode.W))
        {
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
