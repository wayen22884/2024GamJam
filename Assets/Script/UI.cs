using System;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class UI: MonoBehaviour
{
    public dicePool dicePool;
    public Button NextTurn;


    public void PlayEnemyDice(List<Die> enemy,Action onComplete=null)
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

    public void NoDie()
    {
        Debug.LogError("NoDie");
    }

    private void PlayEnemyDie(List<Die.DieFaceTag> dice,Die.DieFaceTag result,Action onComplete=null)
    {
        dicePool.PlayenemyDice(dice,result);
    }
}
[Serializable]
public class dicePool
{
    public List<DiceView> player;
    public DiceView enemy;

    public void PlayenemyDice(List<Die.DieFaceTag> dice,Die.DieFaceTag result,Action onComplete=null)
    {
        enemy.Roll(dice,result);
    }
}