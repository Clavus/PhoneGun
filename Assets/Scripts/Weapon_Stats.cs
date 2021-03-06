﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weapon_Stats : MonoBehaviour 
{
    //private enum WeaponTypes {Main, Options, Extras};
    public LayerMask hitMask;
    public AudioClip fireSound;
    //public float autoDestroy = 3f; 

    /// <summary>
    /// The name of the weapon.
    /// </summary>
    public string _weaponName = "";
	public string WeaponName
	{
		get{return _weaponName;}
		set{_weaponName = value;}
	}

	/// <summary>
	/// The weapon description.
	/// </summary>
	public string _weaponDescription = "";
	public string WeaponDescription
	{
		get{return _weaponDescription;}
		set{_weaponDescription = value;}
	}


	/// <summary>
	/// The weapon types.
	/// </summary>
	public WeaponTypes _weaponTypes;
	public enum WeaponTypes
	{
		Projectile,
		DoTProjectile,
	}   

	/// <summary>
	/// The weapon damage on impact or on DOT
	/// </summary>
	public float _weaponDamage = 1f;
	public float WeaponDamage
	{
		get{return _weaponDamage;}
		set{_weaponDamage = value;}
	}

	/// <summary>
	/// The weapon Damage Over Time (D.O.T) time.
	/// </summary>
	public float _weaponDOTTime = 1f;
	public float WeaponDOTTime
	{
		get{return _weaponDOTTime;}
		set{_weaponDOTTime = value;}
	}

	/// <summary>
	/// The weapon DOT interval. default once per second
	/// </summary>
	public float _weaponDOTInterval = 1f;
	public float WeaponDOTInterval
	{
		get{return _weaponDOTInterval;}
		set{_weaponDOTInterval = value;}
	}

    void OnTriggerEnter(Collider collidedObject)
    {
        // Debug.Log("Detecting hit: objectname: " + colidedObject.name + " On layer: " + colidedObject.gameObject.layer);
        float layerID = Mathf.Log(hitMask.value, 2);

        // Debug.Log("comparing: " + colidedObject.gameObject.layer + " to " + layerID);
        if (collidedObject.gameObject.layer == layerID)
        {
            Debug.Log("i hit: " + collidedObject.name);
        }
        Destroy(this.gameObject);
    }


    void playAudioFile(bool oneShot)
    {
        if (fireSound)
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

    void Awake()
    {
        playAudioFile(true);
    }

    void Start()
    {
        //playAudioFile(true);
    }
}
