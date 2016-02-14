using UnityEngine;
using System.Collections;

public class RedTarget : BaseTargetBehaviour
{

    public float distanceAdd = 5;
    public Color scoreColor = Color.red;

    [SerializeField]
    private ParticleSystem system;

    [SerializeField]
    private GameObject targetObject;

    public override void ReceiveHit(RaycastHit hit)
    {
        //transform.DOPunchScale(Vector3.one * 0.2f, 2f);
        targetObject.SetActive(false);
        GameManager.AddDistance(distanceAdd);

        var scoreBlock = ObjectPool.Get("ScoreBlock", transform.position, Quaternion.identity).GetComponent<ScoreBlock>();
        if (scoreBlock != null)
        {
            scoreBlock.SetScoreText((int) distanceAdd, scoreColor);
            scoreBlock.Launch();
        }

        system.Play();
    }

    public override TargetType GetTargetType()
    {
        return TargetType.Enemy;
    }
}
