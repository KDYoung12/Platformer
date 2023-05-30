using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject shopKey;

    public GameObject npcKey;

    public int keyCount;

    public TextMeshProUGUI goldText;

    // Player의 능력치를 보여주는 Text
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI attackSppedText;

    public Player User;

    public Bullet Arm;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        keyCount = 0;
    }

    private void Update()
    {
        goldText.text = "GOLD : " + User.gold.ToString();
        attackText.text = "Attack : " + Arm.bulletDamage.ToString();
        speedText.text = "Speed : " + User.speed.ToString();
        jumpText.text = "Jump : " + User.jumpPower.ToString();
        defenseText.text = "Defense : " + User.defense.ToString();
        attackSppedText.text = "AttackSpeed : " + User.bulletCoolTimeMax.ToString();
    }

    public void OnShop()
    {
        Debug.Log("OnPKey");
        shopKey.SetActive(true);
    }

    public void OffShop()
    {
        shopKey.SetActive(false);
    }

    public void OnNPC()
    {
        npcKey.SetActive(true);
    }

    public void OffNPC()
    {
        npcKey.SetActive(false);
    }
}
