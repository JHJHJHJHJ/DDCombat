using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHUD : MonoBehaviour
{
    [SerializeField] EnemyLibrary enemyLibrary = null;
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI statusText = null;

    Enemy enemy;
    GamePhase gamePhase;

    [HideInInspector] public int currentIndex = -1;

    private void Awake() 
    {
        enemy = GetComponent<Enemy>();   
        gamePhase = FindObjectOfType<GamePhase>(); 
    }    

    private void Start() 
    {
        ResetHUD();
    }

    public void UpdateTexts()
    {
        UpdateNameText();
        UpdateStatusText();
    }

    public void UpdateNameText()
    {
        if(enemy.isAlive) nameText.text = enemy.enemyName;
        else nameText.text = "";
    }

    public void UpdateStatusText()
    {
        if(enemy.isAlive) statusText.text = "HP" + " " + enemy.hp + "\n" + "ATK" + " " + enemy.atk;
        else statusText.text = "";
    }

    public void SelectEnemy()
    {
        if(gamePhase.phase == Phase.Combat) return;

        EnemyData[] enemies = enemyLibrary.enemies;

        currentIndex++;

        if(currentIndex >= enemies.Length) currentIndex = -1;

        if(currentIndex == -1) enemy.SetEnemy(null);
        else
        {
            enemy.SetEnemy(enemies[currentIndex]);
        }

        UpdateTexts();
    }

    public void ResetHUD()
    {
        currentIndex = -2;
        SelectEnemy();
    }
}
