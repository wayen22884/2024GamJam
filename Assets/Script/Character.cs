using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Character
{
    private List<Die> diceOrigin;
    private List<Die> dice;
    private BaseData baseData;

    private Random random = new();

    public void Initialize(BaseData baseData,List<Die> dice)
    {
        this.baseData = baseData;
        diceOrigin = dice;
    }
    public void StartBattle()
    {
        dice = diceOrigin.ToArray().ToList();
    }
    public List<Die> RollDice(int targetCount)
    {
        int finalCount = Mathf.Min(dice.Count,targetCount);
        var selectedNumbers = Utility.SelectNumbers(random,dice.Count, finalCount);
        var result = new List<Die>();
        foreach (var index in selectedNumbers)
        {
            var die = dice[index];
            result.Add(die);
            die.Roll();
        }
        return result;
    }

}