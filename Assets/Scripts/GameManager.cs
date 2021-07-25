using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GamePhase gamePhase;
    UIController uiController;
    CombatController combatController;

    private void Awake() 
    {
        gamePhase = FindObjectOfType<GamePhase>();
        uiController = FindObjectOfType<UIController>();
        combatController = FindObjectOfType<CombatController>();    
    }

    public void StartCombat()
    {
        if(!uiController.CanStartCombat()) return;

        gamePhase.SetPhase(Phase.Combat);
        uiController.SetUIByPhase(gamePhase.phase);
        combatController.StartCombat();
    }

    public void EndCombat()
    {
        gamePhase.SetPhase(Phase.Ready);
        uiController.SetUIByPhase(gamePhase.phase);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
