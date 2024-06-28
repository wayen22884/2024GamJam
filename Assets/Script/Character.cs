using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Random = System.Random;

public class Character
{
    private string id;
    private List<Die> diceOrigin;
    private Queue<Die> dice = new();
    private Queue<Die> usedDice = new();
    private BaseData baseData;

    private Random random = new();

    private ReactiveProperty<int> health=new();
    private ReactiveProperty<int> attack=new();
    private ReactiveProperty<int> defend=new();
    public int Health => health.Value;
    public int Attack => attack.Value;
    public int Defend => defend.Value;

    public Character(string id)
    {
        this.id = id;
    }
    public void Initialize(BaseData baseData,List<Die> dice)
    {
        health.Subscribe(value => Debug.Log($"{id} Health:{value}"));
        this.baseData = baseData;
        diceOrigin = dice;
        health.Value = this.baseData.MaxHealth;
        attack.Value = this.baseData.Attack;
        defend.Value = this.baseData.Defend;
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

    public void CauseDamage(Character other)
    {
        var damage = other.Attack - Defend;
        health.Value -= damage;
    }

    public void CauseDamageWithDefend(Character other)
    {
        var damage = other.Attack - Defend*2;
        health.Value -= damage;
    }

    public void Recover()
    {
        health.Value += Attack;
    }
}