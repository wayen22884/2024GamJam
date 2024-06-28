using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public List<Draggable> slots;
    public List<Vector2> poitions;

    public event Action<int,int>SwapCallBack;
    public void Initialize()
    {
        foreach (var slot in slots)
        {
            slot.SwapCallBack=SwapSlots;
        }
    }

    public void DecideRange()
    {
        poitions = slots.Where(d => d.gameObject.activeSelf).Select(d => d.rectTransform.anchoredPosition).ToList();
    }

    private bool SwapSlots(Draggable slot1)
    {
        var closestIndex = poitions.Select((num, index) => new { num.x, index })
            .OrderBy(n => Math.Abs(n.x - slot1.rectTransform.anchoredPosition.x))
            .First().index;
        var index= slots.FindIndex(value=>value==slot1);
        if (closestIndex!=index)
        {
            var slot2 = slots[closestIndex];
            
            (slot1.rectTransform.anchoredPosition, slot2.rectTransform.anchoredPosition) = (poitions[closestIndex], poitions[index]);
            
            (slots[index],slots[closestIndex])=(slots[closestIndex],slots[index]);
            SwapCallBack?.Invoke(index,closestIndex);
            return true;
        }
        return false;
    }


}