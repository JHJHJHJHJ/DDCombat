using UnityEngine;

public class GamePhase : MonoBehaviour 
{
    public Phase phase;

    private void Start() 
    {
        phase = Phase.Ready;    
    }

    public void SetPhase(Phase phaseToSet)
    {
        phase = phaseToSet;
    }
}

public enum Phase
{
    Ready, Combat
}