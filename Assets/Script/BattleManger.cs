using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleManger : MonoBehaviour
{
    public UI UI;
    
    public Character player=new("player");
    public Character enemy=new("enemy");
    public bool Test;

    public List<Die> playerResult;
    public List<Die> enemyResult;
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
        
        GameStart();
        EndAndNextTurn();
    }

    private void Initialize()
    {
        UI.NextTurn.onClick.AddListener(Battle);
    }

    private void GameStart()
    {
        player.StartBattle();
        enemy.StartBattle();
    }

    private void Battle()
    {
        var maxLength=Mathf.Max(playerResult.Count,enemyResult.Count);
        var mergedList = Enumerable.Range(0, maxLength)
            .Select(i => (GetDie(playerResult, i),GetDie(enemyResult, i)))
            .ToList();
        foreach (var tuple in mergedList)
        {
            BattleLogic.DoBattle(tuple.Item1,tuple.Item2,player,enemy);
        }
        EndAndNextTurn();
    }

    private Die.DieFaceTag GetDie(List<Die> result, int i)
    {
        if (i>=result.Count)
        {
            return Die.DieFaceTag.None;
        }
        return result[i].resultTag;
    }


    private void EndAndNextTurn()
    {
        player.RecycleDice();
        enemy.RecycleDice();
        (playerResult,enemyResult)= RollDice();
        UI.PlayPlayerDice(playerResult);
        UI.PlayEnemyDice(enemyResult);
    }

    private (List<Die> player,List<Die> enemy) RollDice()
    {
        return (player.RollDice(2), enemy.RollDice(1));
    }
    [ContextMenu("RollEnemyDice")]
    public void RollEnemyDice()
    {
        if (enemy.HasDice)
        {
            var die= enemy.RollDie();
            UI.PlayEnemyDie(die);
        }
        else
        {
            UI.NoDie();
        }
    }
    
}

public static class BattleLogic
{
    public static void DoBattle(Die.DieFaceTag playerDieResultTag, Die.DieFaceTag enemyDieResultTag, Character player,
        Character enemy)
    {
        switch (playerDieResultTag)
        {
            case Die.DieFaceTag.None:
                switch (enemyDieResultTag)
                {
                    case Die.DieFaceTag.None:
                        break;
                    case Die.DieFaceTag.Attack:
                        player.CauseDamage(enemy);
                        break;
                    case Die.DieFaceTag.Dodge:
                        break;
                    case Die.DieFaceTag.Defend:
                        break;
                    case Die.DieFaceTag.Recover:
                        enemy.Recover();
                        break;
                }
                break;
            case Die.DieFaceTag.Attack:
                switch (enemyDieResultTag)
                {
                    case Die.DieFaceTag.None:
                        enemy.CauseDamage(player);
                        break;
                    case Die.DieFaceTag.Attack:
                        enemy.CauseDamage(player);
                        player.CauseDamage(enemy);
                        break;
                    case Die.DieFaceTag.Dodge:
                        break;
                    case Die.DieFaceTag.Defend:
                        player.CauseDamageWithDefend(enemy);
                        break;
                    case Die.DieFaceTag.Recover:
                        enemy.CauseDamage(player);
                        enemy.Recover();
                        break;
                }
                break;
            case Die.DieFaceTag.Dodge:
                switch (enemyDieResultTag)
                {
                    case Die.DieFaceTag.None:
                        break;
                    case Die.DieFaceTag.Attack:
                        break;
                    case Die.DieFaceTag.Dodge:
                        break;
                    case Die.DieFaceTag.Defend:
                        break;
                    case Die.DieFaceTag.Recover:
                        enemy.Recover();
                        break;
                }
                break;
            case Die.DieFaceTag.Defend:
                switch (enemyDieResultTag)
                {
                    case Die.DieFaceTag.None:
                        break;
                    case Die.DieFaceTag.Attack:
                        player.CauseDamageWithDefend(enemy);
                        break;
                    case Die.DieFaceTag.Dodge:
                        break;
                    case Die.DieFaceTag.Defend:
                        break;
                    case Die.DieFaceTag.Recover:
                        enemy.Recover();
                        break;
                }
                break;
            case Die.DieFaceTag.Recover:
                switch (enemyDieResultTag)
                {
                    case Die.DieFaceTag.None:
                        player.Recover();
                        break;
                    case Die.DieFaceTag.Attack:
                        player.CauseDamage(enemy);
                        player.Recover();
                        break;
                    case Die.DieFaceTag.Dodge:
                        player.Recover();
                        break;
                    case Die.DieFaceTag.Defend:
                        player.Recover();
                        break;
                    case Die.DieFaceTag.Recover:
                        player.Recover();
                        break;
                }
                break;
        }
    }
}

