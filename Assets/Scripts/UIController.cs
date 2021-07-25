using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI phaseText = null;
    [SerializeField] Button startButton = null;
    [SerializeField] GameObject inputs = null;

    [Space]
    [SerializeField] GameObject combatMessage = null;
    [SerializeField] TextMeshProUGUI combatMessageText = null;
    [SerializeField] GameObject nextButton = null;
    [SerializeField] GameObject endButton = null;
    [SerializeField] GameObject restartButton = null;

    [Space]
    [SerializeField] EnemyHUD[] enemyHUDs = null;

    UserHUD userHUD;

    private void Awake() 
    {
        userHUD = FindObjectOfType<UserHUD>();    
    }

    private void Update() 
    {
        ChangeStartButtonColor();
    }

    public void SetUIByPhase(Phase phase)
    {
        UpdatePhaseText(phase);

        bool on = true;

        if(phase == Phase.Combat)
        {
            on = false;
            TurnOffNullEnemies();
            userHUD.SetUserDataByInput();
        }
        else if(phase == Phase.Ready)
        {
            userHUD.SetInputFields();

            foreach(EnemyHUD enemyHUD in enemyHUDs)
            {
                enemyHUD.ResetHUD();
                enemyHUD.gameObject.SetActive(true);
            }
            SwitchEndButton(false);
        }

        SwitchStartButton(on);
        SwitchInputs(on);
        SwitchCombatMessage(!on);
        SwitchNextButton(!on);
    }

    public void UpdatePhaseText(Phase phase)
    {
        string phaseToSet = "";
        if(phase == Phase.Ready) phaseToSet = "준비";
        else if (phase == Phase.Combat) phaseToSet = "전투";

        phaseText.text = phaseToSet;
    }

    public void SwitchStartButton(bool on)
    {
        startButton.gameObject.SetActive(on);
    }

    public void SwitchInputs(bool on)
    {
        inputs.SetActive(on);
    }

    public void SwitchCombatMessage(bool on)
    {
        combatMessage.SetActive(on);
    }

    public void SetCombatMessage(string messageText)
    {
        combatMessageText.text = messageText;
    }

    public void SwitchNextButton(bool on)
    {
        nextButton.SetActive(on);
    }

    public void SwitchEndButton(bool on)
    {
        endButton.SetActive(on);
    }

    public void SwitchRestartButton(bool on)
    {
        restartButton.SetActive(on);
    }

    public void TurnOffNullEnemies()
    {
        foreach(EnemyHUD hud in enemyHUDs)
        {
            if(hud.GetComponent<Enemy>().isAlive == false)
            {
                hud.gameObject.SetActive(false);
            }
        }
    }

    public bool CanStartCombat()
    {
        bool canStart = false;

        foreach(EnemyHUD hud in enemyHUDs)
        {
            if(hud.currentIndex != -1)
            {
                canStart = true;
                break;
            }
        }

        return canStart;
    }

    void ChangeStartButtonColor()
    {
        ProceduralImage bg = startButton.GetComponent<ProceduralImage>();

        if(CanStartCombat()) bg.color = new Color32(255, 255, 255, 255);
        else bg.color = new Color32(50, 50, 50, 255);
    }

    public Enemy[] GetEnemies()
    {
        Enemy[] enemies = new Enemy[enemyHUDs.Length];

        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = enemyHUDs[i].GetComponent<Enemy>();
        } 

        return enemies;
    }
}
