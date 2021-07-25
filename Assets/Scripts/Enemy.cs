using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isAlive = false;
    // public bool isAlive = true;

    public string enemyName;
    public int hp;
    public int atk;

    public void SetEnemy(EnemyData enemyData)
    {
        if (enemyData == null) 
        {
            isAlive = false;
            // isAlive = false;
        }
        else
        {
            isAlive = true;
            // isAlive = true;

            enemyName = enemyData.enemyName;
            hp = enemyData.hp;
            atk = enemyData.atk;
        }
    }

    public void GetDamage(int damage)
    {
        hp = Mathf.Clamp(hp - damage, 0, hp);
    }
}
