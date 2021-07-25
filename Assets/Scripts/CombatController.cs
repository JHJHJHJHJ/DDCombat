using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] float waitTime = 0.5f;

    UIController uiController;
    User user;
    UserHUD userHUD;

    [SerializeField] List<Enemy> enemiyList;
    [SerializeField] string[] idleMessages = null;

    private void Awake() 
    {
        uiController = FindObjectOfType<UIController>();    
        user = FindObjectOfType<User>();
        userHUD = FindObjectOfType<UserHUD>();
    }

    public void StartCombat()
    {
        uiController.SetCombatMessage(user.userName +"은(는) " + "전투를 시작했다!");

        AddEnemiesToList();
    }

    void AddEnemiesToList()
    {
        enemiyList = new List<Enemy>();

        foreach(Enemy enemy in uiController.GetEnemies())
        {
            if(enemy.isAlive)
            {
                enemiyList.Add(enemy);
            }   
        }
    }

    public void HandleNextTurn() // Button
    {
        StartCoroutine(HandleCombat());
    }

    IEnumerator HandleCombat()
    {
        uiController.SwitchNextButton(false);

        //Player Turn
        yield return StartCoroutine(HandlePlayerTurn());

        //All Enemies Died?
        if(enemiyList.Count <= 0)
        {
            yield return StartCoroutine(HandleWin());
        }
        else
        {
            //Enemies Turn
            yield return StartCoroutine(HandleEnemiesTurn());

            //User Died?
            if(user.currentHP <= 0)
            {
                yield return StartCoroutine(HandleLose());
            }
            else
            {
                string idleMessage = idleMessages[Random.Range(0,idleMessages.Length)];
                yield return StartCoroutine(SetCombatMessage(user.userName + idleMessage));
                uiController.SwitchNextButton(true);
            }
        }
    }

    IEnumerator HandlePlayerTurn()
    {
        yield return StartCoroutine(SetCombatMessage(user.userName + "의 턴!"));

        userHUD.GetComponent<Animator>().SetTrigger("Attack");
        yield return StartCoroutine(SetCombatMessage(user.userName + "의 공격!"));

        Enemy target = AttackFirstEnemy();
        target.GetComponent<Animator>().SetTrigger("Hurt");
        yield return StartCoroutine(SetCombatMessage(target.enemyName + "에게 " + user.atk + " 데미지!"));

        if (target.hp <= 0)
        {
            KillEnemy(target);
            yield return StartCoroutine(SetCombatMessage(target.enemyName + "을 쓰러뜨렸다!"));
        }
    }

    private Enemy AttackFirstEnemy()
    {
        Enemy target = enemiyList[0];
        target.GetDamage(user.atk);
        target.GetComponent<EnemyHUD>().UpdateStatusText();

        return target;
    }

    private void KillEnemy(Enemy target)
    {
        target.isAlive = false;
        uiController.TurnOffNullEnemies();
        enemiyList.Remove(target);
    }

    IEnumerator HandleEnemiesTurn()
    {
        yield return StartCoroutine(SetCombatMessage("몬스터 턴!"));

        foreach(Enemy enemy in enemiyList)
        {
            enemy.GetComponent<Animator>().SetTrigger("Attack");
            yield return StartCoroutine(SetCombatMessage(enemy.enemyName + "의 공격!"));
            
            user.GetDamage(enemy.atk);
            userHUD.UpdateUserTexts();
            userHUD.GetComponent<Animator>().SetTrigger("Hurt");
            yield return StartCoroutine(SetCombatMessage(user.userName + "은(는) " + enemy.atk + " 데미지를 입었다!"));

            if(user.currentHP <= 0) break;
        }
    }

    IEnumerator HandleWin()
    {
        yield return StartCoroutine(SetCombatMessage("전투에서 승리했다!"));
        uiController.SwitchEndButton(true);
    }

    IEnumerator HandleLose()
    {
        yield return StartCoroutine(SetCombatMessage(user.userName + "은(는) 쓰러졌다..."));
        uiController.SwitchRestartButton(true);
    }

    IEnumerator SetCombatMessage(string message)
    {
        uiController.SetCombatMessage(message);
        yield return new WaitForSeconds(waitTime);
    }
}
