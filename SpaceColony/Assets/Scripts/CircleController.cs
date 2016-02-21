using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CircleController : MonoBehaviour {

    [SerializeField]
    Image circle;
    [SerializeField]
    Canvas canvas;

    RectTransform rectTransform;

    float time;

    /// <summary>
    /// 円の表示比率をセット
    /// </summary>
    /// <param name="ratio">0 - 100</param>
    public void SetCircleRatio(int ratio)
    {
        if (ratio > 100) ratio = 100;
        if (ratio < 0) ratio = 0;

        var heigt = rectTransform.sizeDelta.y;
        var canvasY = canvas.GetComponent<RectTransform>().sizeDelta.y;
        var posY = Screen.height - Mathf.Abs(rectTransform.position.y);
        var diff = rectTransform.position.y - rectTransform.localPosition.y;

        var circleRatio = diff + posY + heigt * (float)ratio / 100.0f;

        circle.material.SetFloat("_CircleRatio", circleRatio);
    }

    void Awake()
    {
        rectTransform = circle.GetComponent<RectTransform>();
    }

    void Update()
    {
        time += Time.deltaTime;

        SetCircleRatio((int)time * 10);
    }
}
