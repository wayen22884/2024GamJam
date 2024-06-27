using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public static class Utility
{
    private static Random random = new();

    private static Dictionary<Die.DieFaceTag, Sprite> sprites;
    public static void Initialize()
    {
         var data= Resources.Load<FaceTags>("FaceTags");
         sprites = data.List.ToDictionary(p => p.DieFace, p => p.Sprite);   
    }
    public static List<int> SelectNumbers(Random random,int range, int count)
    {
        var numbers = new List<int>(range);
        for (int i = 0; i < range; i++)
        {
            numbers.Add(i);
        }

        var selectedNumbers = numbers.OrderBy(x => random.Next()).Take(count).ToList();
        return selectedNumbers;
    }
    public static List<int> SelectNumbers(int range, int count)
    {
        return SelectNumbers(random, range, count);
    }
    public static Sprite GetSprite(Die.DieFaceTag tag)
    {
        return sprites[tag];
    }
    
}