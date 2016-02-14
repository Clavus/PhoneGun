using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : SingletonComponent<GameManager>
{

    public float distanceLeft;
    public float distanceTravelled;

    [SerializeField]
    private Text distanceLeftText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text speedText;

    [SerializeField]
    private Vehicle playerVehicle;
    
    private Vector3 lastVehiclePos;

	void Start ()
	{
	    lastVehiclePos = playerVehicle.transform.position;
	}
	
	void Update ()
	{
        float travelled = (playerVehicle.transform.position - lastVehiclePos).magnitude;
	    distanceLeft -= travelled;
	    distanceTravelled += travelled;
        lastVehiclePos = playerVehicle.transform.position;
        distanceLeftText.text = distanceLeft.ToString("0") + "m";
	    scoreText.text = distanceTravelled.ToString("0") + "m";
	    speedText.text = (playerVehicle.speed * 3.6f).ToString("0") + "km/h";

        if (distanceLeft <= 0)
	    {
	        distanceLeft = 0;
	        playerVehicle.speed = 0;
	    }


        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene("MainScene");

	}

    public static void AddDistance(float distance)
    {
        instance.distanceLeft += distance;
    }

}
