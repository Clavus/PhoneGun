using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBlock : MonoBehaviour
{

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private AudioSource scoreSound;


    private Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

	void OnDisable()
	{
	    body.velocity = Vector3.zero;
	    scoreText.text = "";
	}

    public void SetScoreText(int score, Color col)
    {
        scoreText.text = "+" + score + "m";
        scoreText.color = col;
    }

    public void Launch()
    {
        if (body != null)
        {
            body.AddForce(Vector3.up * 6, ForceMode.Impulse);
            body.AddTorque(0,Random.Range(-6f,6f), Random.Range(-6f, 6f));
            scoreSound.Play();
        }
            
    }

}
