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
    private GameObject gameOverObject;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Vehicle playerVehicle;
    
    private Vector3 lastVehiclePos;

    private bool gameOver = false;

	void Start ()
	{
	    lastVehiclePos = playerVehicle.transform.position;
        gameOverObject.SetActive(false);

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
	        gameOver = true;
            gameOverObject.SetActive(true);
	        gameOverText.text = gameOverText.text.Replace("%NUM%", distanceTravelled.ToString("0"));
	    }


	    if (Input.GetKeyDown(KeyCode.F1))
	    {
	        TouchDeviceSocketListener.CloseSocket();
            SceneManager.LoadScene("MainScene");
        }

	}

    public static void AddDistance(float distance)
    {
        instance.distanceLeft += distance;
    }

    public static bool IsGameOver()
    {
        return instance.gameOver;
    }

}
