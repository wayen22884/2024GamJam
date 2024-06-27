using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterValue", menuName = "CreateCharacterValue")]
public class CharacterValue: ScriptableObject
{
    public int MaxHealth;
    public int Attack;
    public int Defend;
    public List<Die> Dice = new();

    public BaseData GetBaseData => new BaseData() { MaxHealth = MaxHealth, Attack = Attack, Defend = Defend };
    public List<Die> GetDice => Dice.ToArray().ToList();
}