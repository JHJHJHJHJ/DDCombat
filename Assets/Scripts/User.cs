using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public bool isAlive = true;

    public string userName = "";
    public int currentHP;
    public int maxHP;
    public int atk;

    public void GetDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
    }
}
