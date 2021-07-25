using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "DDCombat/EnemyData", order = 0)]
public class EnemyData : ScriptableObject 
{
    public string enemyName;
    public int hp;
    public int atk;  
}