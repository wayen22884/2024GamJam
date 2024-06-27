using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class DiceView : MonoBehaviour
{
    public Image Image;

    public void Roll(List<Die.DieFaceTag> dice,Die.DieFaceTag result,Action onComplete=null)
    {
        Observable.Interval(TimeSpan.FromSeconds(0.05))
            .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(2)))
            .Subscribe(_ =>
            {
                var result = Utility.SelectNumbers(dice.Count, 1);
                Image.sprite = Utility.GetSprite(dice[result[0]]);
            },onCompleted: () =>
            {
                Image.sprite = Utility.GetSprite(result);
                onComplete?.Invoke();
            })
            .AddTo(this);
    }

    public List<Die.DieFaceTag> TestDieFaces;
    public Die.DieFaceTag resultFace;
    [ContextMenu("TestRoll")]
    private void Test()
    {
        Roll(TestDieFaces,resultFace);
    }
}
