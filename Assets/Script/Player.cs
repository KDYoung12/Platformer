using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed;

    public float jumpPower;

    public float damage;

    public Transform firePos;

    public GameObject bullet;

    Animator anim;
    Rigidbody2D rigid2D;
    SpriteRenderer sprite;

    int _jumpCnt;
    int _maxJumpCount;
    float bulletCoolTime;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        bulletCoolTime = 0.2f;
        _jumpCnt = 0;
        _maxJumpCount = 1;
    }
    void Update()
    {
        bulletCoolTime -= Time.deltaTime;
        float h = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        transform.Translate(new Vector2(h, 0));
        anim.SetBool("isRun", true);

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

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCnt < _maxJumpCount)
        {
            rigid2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            _jumpCnt++;
            Debug.Log(_jumpCnt);
        }

        if (Input.GetKey(KeyCode.Z) && bulletCoolTime <= 0f)
        {
            Instantiate(bullet, firePos.position, Quaternion.identity);
            bulletCoolTime = 0.2f;
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
