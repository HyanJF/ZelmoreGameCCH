using DG.Tweening;
using UnityEngine;

public class RotateUI : MonoBehaviour
{
    public float duration = 2f;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
