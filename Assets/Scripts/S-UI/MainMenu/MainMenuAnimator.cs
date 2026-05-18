using UnityEngine;
using DG.Tweening;

public class MainMenuAnimator : MonoBehaviour
{
    [Header("Referencias")]
    public RectTransform title;
    public CanvasGroup[] buttons;

    private Sequence currentSequence;
    [SerializeField]
    private MenuAnimationState currentState;

    private void Start()
    {
        PlayAnimation(MenuAnimationState.Start);
    }

    // Sistema Central
    public void PlayAnimation(MenuAnimationState state)
    {
        if (currentState == state) return;

        KillAllAnimations();
        currentState = state;

        switch (state)
        {
            case MenuAnimationState.Start:
                PlayStart();
                break;
            case MenuAnimationState.Enter:
                PlayEnter();
                break;
            case MenuAnimationState.Idle:
                PlayIdle();
                break;
        }
    }

    // START
    void PlayStart()
    {
        Sequence seq = DOTween.Sequence();

        ResetUI();

        seq.Append(title.DOScale(1.2f, 0.5f).SetEase(Ease.OutBack));
        seq.Append(title.DOScale(1f, 0.3f));

        AnimateButtons(seq);

        seq.AppendCallback(() =>
        {
            EnableButtons(true);
            PlayAnimation(MenuAnimationState.Idle);
        });

        currentSequence = seq;
    }

    // ENTER
    public void PlayEnter()
    {
        Sequence seq = DOTween.Sequence();

        ResetUI();

        seq.Append(title.DOScale(1f, 0.1f).From(0).SetEase(Ease.OutBack));

        AnimateButtons(seq);

        seq.AppendCallback(() =>
        {
            EnableButtons(true);
            PlayAnimation(MenuAnimationState.Idle);
        });

        currentSequence = seq;
    }

    // IDLE
    void PlayIdle()
    {
        foreach (var btn in buttons)
        {
            btn.transform.DOScale(1.05f, 1.2f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetId("Idle");
        }
    }

    // CHANGE 
    public void PlayChange(System.Action onComplete = null)
    {
        KillAllAnimations();

        Sequence seq = DOTween.Sequence();

        EnableButtons(false);
        KillIdle();

        AnimateExit(seq);

        seq.AppendCallback(() =>
        {
            onComplete?.Invoke();
        });

        currentSequence = seq;
    }

    // EXIT
    public void PlayExit()
    {
        KillAllAnimations();

        Sequence seq = DOTween.Sequence();

        EnableButtons(false);
        KillIdle();

        AnimateExit(seq);

        seq.AppendCallback(QuitGame);

        currentSequence = seq;
    }

    // REUTILIZABLES

    void AnimateButtons(Sequence seq)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            seq.Append(buttons[i].DOFade(1f, 0.2f));
            seq.Join(buttons[i].transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack));
            seq.AppendInterval(0.1f);
        }
    }

    void AnimateExit(Sequence seq)
    {
        for (int i = buttons.Length - 1; i >= 0; i--)
        {
            seq.Append(buttons[i].DOFade(0f, 0.15f));
            seq.Join(buttons[i].transform.DOScale(0f, 0.2f).SetEase(Ease.InBack));
        }

        seq.Append(title.DOScale(0f, 0.3f).SetEase(Ease.InBack));
    }

    void ResetUI()
    {
        title.localScale = Vector3.zero;

        foreach (var btn in buttons)
        {
            btn.alpha = 0;
            btn.transform.localScale = Vector3.zero;
            btn.interactable = false;
            btn.blocksRaycasts = false;
        }
    }

    void EnableButtons(bool value)
    {
        foreach (var btn in buttons)
        {
            btn.interactable = value;
            btn.blocksRaycasts = value;
        }
    }

    void KillAllAnimations()
    {
        if (currentSequence != null)
            currentSequence.Kill();

        DOTween.Kill("Idle");

        title.DOKill();

        foreach (var btn in buttons)
        {
            btn.transform.DOKill();
        }
    }

    void KillIdle()
    {
        DOTween.Kill("Idle");
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}