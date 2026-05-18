using DG.Tweening;
using UnityEngine;

public class GenericMenuAnimator : MonoBehaviour
{
    public RectTransform[] elements;

    private Sequence currentSequence;

    public string idleID = "MenuIdle";

    // ENTRADA
    public void PlayEnter()
    {
        KillAll();

        Sequence seq = DOTween.Sequence();

        foreach (var el in elements)
        {
            CanvasGroup cg = el.GetComponent<CanvasGroup>();

            el.localScale = Vector3.zero;
            cg.alpha = 0;
        }

        for (int i = 0; i < elements.Length; i++)
        {
            CanvasGroup cg = elements[i].GetComponent<CanvasGroup>();

            seq.Append(cg.DOFade(1f, 0.1f));
            seq.Join(elements[i].DOScale(1f, 0.25f).SetEase(Ease.OutBack));
            seq.AppendInterval(0.05f);
        }

        seq.AppendCallback(PlayIdle);

        currentSequence = seq;
    }

    // IDLE
    void PlayIdle()
    {
        foreach (var el in elements)
        {
            el.DOScale(1.03f, 1.5f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetId(idleID);
        }
    }

    // CHANGE
    public void PlayChange(System.Action onComplete = null)
    {
        KillAll();

        Sequence seq = DOTween.Sequence();

        for (int i = elements.Length - 1; i >= 0; i--)
        {
            CanvasGroup cg = elements[i].GetComponent<CanvasGroup>();

            seq.Append(cg.DOFade(0f, 0.15f));
            seq.Join(elements[i].DOScale(0f, 0.2f).SetEase(Ease.InBack));
        }

        seq.AppendCallback(() =>
        {
            onComplete?.Invoke();
        });

        currentSequence = seq;
    }

    void KillAll()
    {
        if (currentSequence != null)
            currentSequence.Kill();

        DOTween.Kill(idleID);

        foreach (var el in elements)
        {
            el.DOKill();
        }
    }
}