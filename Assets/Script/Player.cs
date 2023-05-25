using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float playerHp;

    public float playerMaxHp;

    public float speed;

    public float jumpPower;

    public float damage;

    public float defense;

    public int gold;

    public Transform firePos;

    public GameObject bullet;

    public GameObject shop;

    public Slider playerHpBar;

    public float climbSpeed;

    private bool isClimbing = false;

    private bool isLadder;

    Animator anim;
    Rigidbody2D rigid2D;
    SpriteRenderer sprite;
    Collider2D coll;

    int _jumpCnt;
    int _maxJumpCount;
    float bulletCoolTime;
    void Awake()
    {
        playerHp = playerMaxHp; 

        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }
    void Start()
    {
        bulletCoolTime = 0.2f;
        _jumpCnt = 0;
        _maxJumpCount = 1;
    }
    void Update()
    {
        // .. 사다리
        float verticalInput = Input.GetAxis("Vertical");

        if (isClimbing)
        {
            // 사다리를 올라가는 동작
            float climbMovement = verticalInput * climbSpeed;
            transform.Translate(Vector3.up * climbMovement * Time.deltaTime);
            anim.SetTrigger("onLadder");
        }

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

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCnt < _maxJumpCount && !isLadder)
        {
            rigid2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            _jumpCnt++;
            Debug.Log(_jumpCnt);
        }

        if (Input.GetKey(KeyCode.Z) && bulletCoolTime <= 0f && !isLadder)
        {
            // 프리펩으로 GetComponent를 해주면 기준이 없어짐
            if (sprite.flipX == true)
            {
                GameObject Bullet = Instantiate(bullet, firePos.position, Quaternion.identity) as GameObject;
                Bullet.GetComponent<Bullet>().bulletSpeed *= -1;
            }
            else
            {
                Instantiate(bullet, firePos.position, Quaternion.identity);
            }
            bulletCoolTime = 0.2f;
        }
        playerHpBar.value = playerHp / playerMaxHp;
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
        if (collision.gameObject.CompareTag("Apple"))
        {
            playerHp += 10;
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Shop"))
        {
            shop.SetActive(true);
            GameManager.instance.OnPKey();
            /*
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("PPPP");
                shop.SetActive(true);
            }   
            */
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
            rigid2D.gravityScale = 0f;
            coll.isTrigger = true;
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput != 0f)
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 사다리와 접촉을 끝냈을 경우
        if (collision.gameObject.CompareTag("Ladder"))
        {
            rigid2D.gravityScale = 1.5f;
            coll.isTrigger = false;
            isLadder = false;
            isClimbing = false;
        }

        if (collision.gameObject.CompareTag("Shop"))
        {
            GameManager.instance.OffPKey();
            shop.SetActive(false);
        }
    }
}
