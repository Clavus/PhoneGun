using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Enemy_Text : MonoBehaviour {

    public string[] textMessages;
    public bool showTexts;
    public float textInterval = 5f;


    public Text enemyText;

    // Use this for initialization
    void Start () {

        if (showTexts)
        {
            StartCoroutine(ShowTexts());
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator ShowTexts()
    {

        foreach(string message in textMessages)
        {
            //enemyText.GetComponent<Text>().text = message;
            enemyText.text = message;
            yield return new WaitForSeconds(textInterval);
        }
        
    }


}
