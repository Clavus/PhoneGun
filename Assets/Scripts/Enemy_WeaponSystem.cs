 using UnityEngine;
using System.Collections;

public class Enemy_WeaponSystem : MonoBehaviour {

    public GameObject weapon;
    public GameObject fireHartpoint;
    public float fireInterval = 5f;
    public float burstInterval = 0.3f;
    public bool enableBurstMode = true;
    public int photonSalvo = 5;
    public float autoDestroy = 3f;
    public float fireDistance = 6f;

    //([Header],"s");
    public float weaponRotationOffset = 0f;
    public float weaponSpeed = 2000f;
    public AudioClip fireSound;
    public bool enableFireing = true;

    [SerializeField]
    private float distance;

    //do not change these vales
    private float cooldown;
    private float shootTime1 = 0.0f;
    private GameObject weaponClone;


	// Update is called once per frame
	void Update ()
    {
        //CheckForFireing();
        FireSelectedWeapon();
        CheckDistance();
    }

    void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    void FireSelectedWeapon()
    {
        if (distance <= fireDistance)
        {
            if (Time.time >= shootTime1)
            {
                if (fireHartpoint != null)
                {
                    if (enableBurstMode)
                    {
                        StartCoroutine(FireWeaponObjectBurst());
                    }
                    else
                    {
                        FireWeaponObjectNormal();
                    }
                }
                else
                {
                    Debug.Log("[" + this.name + "] " + "Please select a firehartpoint first");
                }

                shootTime1 = Time.time + fireInterval;
            }
        }
    }

    // [A] creates a new instance of the beam or projectyle weapon using Burst fire
    IEnumerator FireWeaponObjectBurst()
    {
        // fire x photons per salvo shot
        for (int i = 0; i < photonSalvo; i++)
        {
            playAudioFile(true);

            // Create a new instance of weapon[currentSeclected], startfrom firehartpoint position, and use firehartpoint Y rotation to [x.y.z])
            weaponClone = (GameObject)Instantiate(weapon, fireHartpoint.transform.position, Quaternion.Euler(0, fireHartpoint.transform.eulerAngles.y + weaponRotationOffset, 0));

            Destroy(weaponClone.gameObject, autoDestroy);

            // Fire weapon at speed x
            weaponClone.GetComponent<Rigidbody>().AddForce(transform.forward * weaponSpeed);
    
            yield return new WaitForSeconds(burstInterval);
        }
    }

    void playAudioFile(bool oneShot)
    {
        if(fireSound)
        {
            if (oneShot)
            {
                GetComponent<AudioSource>().PlayOneShot(fireSound);
            }
            else
            {
                //GetComponent<AudioSource>().Play();
            }
        } 
    }

    // [B] creates a new instance of the beam or projectyle weapon
    void FireWeaponObjectNormal()
    {
        playAudioFile(true);

        // create a new instance of weapon[currentSeclected], starfrom firehartpoint position, and use firehartpoint Y rotation to [x.y.z])
        weaponClone = (GameObject)Instantiate(weapon, fireHartpoint.transform.position, Quaternion.Euler(0, fireHartpoint.transform.eulerAngles.y + weaponRotationOffset, 0));

        Destroy(weaponClone.gameObject, autoDestroy);

        // Fire weapon at speed x
        weaponClone.GetComponent<Rigidbody>().AddForce(transform.forward * weaponSpeed);
    }
}
