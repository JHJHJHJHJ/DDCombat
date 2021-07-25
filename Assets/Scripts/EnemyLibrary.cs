using UnityEngine;

[CreateAssetMenu(fileName = "EnemyLibrary", menuName = "DDCombat/EnemyLibrary", order = 0)]
public class EnemyLibrary : ScriptableObject 
{
    public EnemyData[] enemies = null;    
}