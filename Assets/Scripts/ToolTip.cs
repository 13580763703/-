using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{

    private TMP_Text toolTipText;
    private TMP_Text contentText;
    CanvasGroup convasGroup;
    public float smoothing = 1;
    private float targetAlpha = 1;
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
        convasGroup.alpha = 1;
    }

    public void Hide()
    {
        convasGroup.alpha = 0;
    }

}
