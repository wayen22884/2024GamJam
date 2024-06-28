using System;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class UI: MonoBehaviour
{
    public dicePool dicePool;
    public Button NextTurn;
    public SlotManager SlotManager;

    public void Initialize()
    {
        SlotManager.Initialize();
    }

    private void PlayEnemyDice(List<Die> enemy,Action onComplete=null)
    {
        if (enemy.Count<=0)
        {
            NoDie();
            return;
        }
        var die= enemy[0];
        PlayEnemyDie(die.dieFaceTags,die.resultTag);
    }
    
    public void PlayEnemyDie(Die die,Action onComplete=null)
    {
        PlayEnemyDie(die.dieFaceTags,die.resultTag);
    }

    private void PlayPlayerDice(List<Die> player,Action onComplete=null)
    {
        if (player.Count<=0)
        {
            NoDie();
            return;
        }

        dicePool.PlayPlayerDice(player);
    }


    public void NoDie()
    {
        Debug.LogError("NoDie");
    }

    private void DecideRange()
    {
        SlotManager.DecideRange();
    }
    private void PlayEnemyDie(List<Die.DieFaceTag> faceTags,Die.DieFaceTag result,Action onComplete=null)
    {
        dicePool.PlayEnemyDice(faceTags,result);
    }

    public void ShowDice(List<Die> playerResult, List<Die> enemyResult)
    {
        PlayPlayerDice(playerResult);
        PlayEnemyDice(enemyResult);
        DecideRange();
    }


    public void SetSwapCallBack(Action<int, int> swapPlayerDiceResult)
    {
        SlotManager.SwapCallBack += swapPlayerDiceResult;
        SlotManager.SwapCallBack += dicePool.SwapPlayerDiceResult;
    }
}
[Serializable]
public class dicePool
{
    public List<DiceView> player;
    public DiceView enemy;

    public void PlayEnemyDice(List<Die.DieFaceTag> faceTags,Die.DieFaceTag result,Action onComplete=null)
    {
        enemy.Roll(faceTags,result);
    }

    public void PlayPlayerDice(List<Die> dice)
    {
        if (dice.Count>5)
        {
            Debug.LogError("設計須擴展");
            return;
        }
        ResetPlayerDice();

        for (int i = 0; i < dice.Count; i++)
        {
            var die = dice[i];
            var diceView = player[i];
            diceView.SetActive(true);
            diceView.Roll(die.dieFaceTags,die.resultTag);
        }
    }

    private void ResetPlayerDice()
    {
        foreach (var pDiceView in player)
        {
            pDiceView.SetActive(false);
        }
    }

    public void SwapPlayerDiceResult(int arg1, int arg2)
    {
        (player[arg1], player[arg2]) = (player[arg2], player[arg1]);
    }
}