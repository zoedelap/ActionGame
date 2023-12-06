using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class DynamicInset : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        RectTransform parentRectTransform = rectTransform.parent as RectTransform;
        float parentWidth = parentRectTransform.rect.width;
        float parentHeight = parentRectTransform.rect.height;
        rectTransform.offsetMin = new Vector2(parentWidth * 0.25f, parentHeight * 0.25f); // Left, Bottom
        rectTransform.offsetMax = new Vector2(-parentWidth * 0.25f, -parentHeight * 0.25f); // Right, Top
    }
}
