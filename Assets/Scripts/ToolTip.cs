using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class ToolTip : MonoBehaviour
{

    private TMP_Text toolTipText;
    private TMP_Text contentText;
    CanvasGroup convasGroup;
    public float smoothing = 1;
    private float targetAlpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        toolTipText = GetComponent<TMP_Text>();
        contentText = transform.Find("Content").GetComponent<TMP_Text>();
        convasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(convasGroup.alpha != targetAlpha)
        {
            convasGroup.alpha = Mathf.Lerp(convasGroup.alpha, targetAlpha, smoothing*Time.deltaTime);
            if(Mathf.Abs(convasGroup.alpha-targetAlpha) <= 0.01f)
            {
                convasGroup.alpha = targetAlpha;
            }
        }
    }

    public void Show(string text)
    {
        toolTipText.text = text;
        contentText.text = text;
        targetAlpha = 1;
    }

    public void Hide()
    {
        targetAlpha = 0;
    }

    public void SetLocalPosition(Vector2 position)
    {
        this.transform.localPosition= position;
    }
}
