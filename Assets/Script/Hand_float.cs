using System;
using UnityEngine;

public class Hand_float : MonoBehaviour
{
    [Header("浮动动画")]
    public float floatHight = 20f;
    public float floatSpeed = 2f;

    private RectTransform rectTransform;
    private Vector2 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = (float)(startPos.y + Math.Sin(Time.time * floatSpeed) * floatHight);
        rectTransform.anchoredPosition = new Vector2(startPos.x, newY);
    }
}
