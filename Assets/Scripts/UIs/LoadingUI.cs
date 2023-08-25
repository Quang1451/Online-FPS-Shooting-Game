using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LoadingUI : UIBasic
{
    [SerializeField] private TMP_Text textPercent;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private float duration = 0.2f;

    public override void BeginHide()
    {
        base.BeginHide();
        OnHideComplete();
    }

    public void SetPercent(float value)
    {
        loadingBar.DOValue(value, duration).OnUpdate(() =>
        {
            textPercent.text = $"{(value * 100).ToString("0")}%";
        });
    }
}
