using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _jumpPower;

    [SerializeField]
    private TextMeshProUGUI _keyText;

    Animator anim;
    Rigidbody2D rigid2D;
    SpriteRenderer sprite;

    int _jumpCnt;
    int _maxJumpCount;
    int score;
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
        score = 0;
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
            rigid2D.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {   
            score++;
            collision.gameObject.SetActive(false);
            _keyText.text = "Key : " + score;
        }
    }
}
