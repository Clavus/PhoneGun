using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ReloadPanel : BaseTargetBehaviour
{
    [SerializeField]
    public BulletBelt bulletBelt;

    [SerializeField]
    private AudioSource reloadAudio;

    [SerializeField]
    private Transform reloadTextCanvasTransform;

    private Vector3 baseTextTransform;

    void Start()
    {
        baseTextTransform = reloadTextCanvasTransform.localScale;
    }

    public override void ReceiveHit(RaycastHit hit)
    {
        reloadAudio.Play();
        bulletBelt.Reload();

        reloadTextCanvasTransform.DOKill();
        reloadTextCanvasTransform.localScale = baseTextTransform;
        reloadTextCanvasTransform.DOPunchScale(baseTextTransform * 1.05f, 0.5f);
    }

    public override TargetType GetTargetType()
    {
        return TargetType.Trigger;
    }
}
