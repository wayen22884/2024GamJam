using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Character
{
    private List<Die> diceOrigin;
    private Queue<Die> dice = new();
    private Queue<Die> usedDice = new();
    private BaseData baseData;

    private Random random = new();

    public void Initialize(BaseData baseData,List<Die> dice)
    {
        this.baseData = baseData;
        diceOrigin = dice;
    }
    public void StartBattle()
    {
        var list = diceOrigin.OrderBy(x => random.Next()).ToList();
        list.ForEach(die => dice.Enqueue(die));
    }
    public List<Die> RollDice(int targetCount)
    {
        int finalCount = Mathf.Min(dice.Count,targetCount);
        var result = new List<Die>();

        for (int i = 0; i < finalCount; i++)
        {
            result.Add(RollDie());
        }
        return result;
    }

    public bool HasDice => dice.Count > 0;
    public Die RollDie()
    {
        var die = dice.Dequeue();
        usedDice.Enqueue(die);
        die.Roll();
        return die;
    }

    public void RecycleDice()
    {
        var list = usedDice.OrderBy(x => random.Next()).ToList();
        usedDice.Clear();
        list.ForEach(die => dice.Enqueue(die));
    }
}