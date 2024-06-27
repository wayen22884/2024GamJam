using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleManger : MonoBehaviour
{
    public UI UI;
    
    public Character player=new();
    public Character enemy=new();
    public bool Test;
    private void Awake()
    {
        Utility.Initialize();
        if (Test)
        {
            var playerData= Resources.Load<CharacterValue>("PlayerValue");
            var enemyData = Resources.Load<CharacterValue>("EnemyValue");
            player.Initialize(playerData.GetBaseData,playerData.GetDice);
            enemy.Initialize(enemyData.GetBaseData,enemyData.GetDice);
        }

        Initialize();
        
        StartBattle();
        StartTurn();
    }

    private void Initialize()
    {
        UI.NextTurn.onClick.AddListener(StartTurn);
    }

    private void StartBattle()
    {
        player.StartBattle();
        enemy.StartBattle();
    }
    private void StartTurn()
    {
        var result= RollDice();
        UI.PlayEnemyDice(result.enemy);
    }

    private (List<Die> player,List<Die> enemy) RollDice()
    {
        return (player.RollDice(2), enemy.RollDice(1));
    }
}