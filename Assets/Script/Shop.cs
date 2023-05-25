using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;

    private int jumpCount, attackCount, speedCount, defenseCount;

    public void OnClickHp()
    {
        if (GetComponent<Player>().playerHp >= 100)
        {
            return;
        }
        else
        {
            GetComponent<Player>().playerHp += 30f;
            GetComponent<Player>().gold -= 75;
        }
    }
    public void OnClickJump()
    {
        if (jumpCount > 2)
            return;
        else
        {
            jumpCount++;
            GetComponent<Player>().jumpPower += 1f;
            GetComponent<Player>().gold -= 50;
        }
    }
    public void OnClickAttack()
    {
        if (attackCount > 2)
            return;
        else
        {
            attackCount++;
            GetComponent<Bullet>().bulletDamage += 1f;
            GetComponent<Player>().gold -= 100;
        }
    }
    public void OnClickDefender()
    {
        if (defenseCount > 2)
            return;
        else
        {
            defenseCount++;
            GetComponent<Player>().defense += 5f;
            GetComponent<Player>().gold -= 50;
        }
    }
    public void OnClickSpeed()
    {
        if (speedCount > 2)
            return;
        else
        {
            speedCount++;
            GetComponent<Player>().speed += 2f;
            GetComponent<Player>().gold -= 60;
        }
    }
    public void OnClickExit()
    {
        shopUI.SetActive(false);
    }
}
