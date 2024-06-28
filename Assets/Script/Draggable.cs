using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public Func<Draggable,bool> SwapCallBack;

    private Vector2 originalPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        var result= SwapCallBack?.Invoke(this)??false;
        Debug.LogError(result);
        if (!result)
        {
            var position = new Vector3(originalPosition.x, originalPosition.y, 0);
            transform.DOLocalMove(position,1f);
        }

        originalPosition = rectTransform.anchoredPosition;
    }
    

}