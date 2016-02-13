using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MoveOverLocalPath : MonoBehaviour
{

    public float pathDuration = 2f;

    [SerializeField]
    private Vector3[] localPath;

	// Use this for initialization
	void Start ()
	{
	    DoPath();
	}
	
    void DoPath()
    {
        transform.DOLocalPath(localPath, pathDuration, PathType.CatmullRom, PathMode.Ignore)
            .OnComplete(DoPath);
    }


}
