 using UnityEngine;
using System.Collections;

public class Enemy_WeaponSystem : MonoBehaviour {

    public GameObject[] weapons;
    public GameObject fireHartpoint;
    //([Header],"s");
    public float weaponRotationOffset = 0f;
    public float weaponSpeed = 2000f;
    public AudioClip fireSound;
    public bool enableFireing = true;


    //do not change these vales
    private float cooldown;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //CheckForFireing();
    }

    IEnumerator FireWeapon()
    {
        while (enableFireing)
        {

            yield return new WaitForSeconds(textInterval);
        }
    }
}
