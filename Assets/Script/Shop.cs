using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;

    private int jumpCount, attackCount, speedCount, defenseCount;

    public Player User;

    public Bullet Arm;

    private void Start()
    {
        Arm = GameObject.FindWithTag("Bullet").GetComponent<Bullet>();
        User = GameObject.FindWithTag("Player").GetComponent<Player>();
        Debug.Log("Player HP =" + User.playerHp.ToString());
    }

    public void OnClickHp()
    {
        if (User.playerHp >= 100)
        {
            return;
        }

        else if (User.gold <= 76) {
            Debug.Log("No Money");
            return;
        }
            
        else
        {
            User.playerHp += 30f;
            User.gold -= 75;
        }
    }
    public void OnClickJump()
    {
        if (jumpCount > 2)
            return;

        else if (User.gold <= 51)
        {
            Debug.Log("No Money");
            return;
        }

        else
        {
            jumpCount++;
            User.jumpPower += 0.2f;
            User.gold -= 50;
        }
    }
    public void OnClickAttack()
    {
        if (attackCount > 1 )
            return;

        else if (User.gold <= 101)
        {
            Debug.Log("No Money");
            return;
        }

        else
        {
            attackCount++;
            Arm.bulletDamage += 1f;
            User.bulletCoolTimeMax -= 0.05f;
            User.gold -= 100;
        }
    }
    public void OnClickDefender()
    {
        if (defenseCount > 2)
            return;

        else if (User.gold <= 126)
        {
            Debug.Log("No Money");
            return;
        }

        else
        {
            defenseCount++;
            User.defense += 5f;
            User.gold -= 125;
        }
    }
    public void OnClickSpeed()
    {
        if (speedCount > 2)
            return;

        else if (User.gold <= 61)
        {
            Debug.Log("No Money");
            return;
        }

        else
        {
            speedCount++;
            User.speed += 1f;
            User.gold -= 60;
        }
    }
    public void OnClickExit()
    {
        shopUI.SetActive(false);
    }
}
