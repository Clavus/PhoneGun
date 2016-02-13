using UnityEngine;
using System.Collections;

public class Weapon_Health : MonoBehaviour {

    public float startHealth = 10f;
    public GameObject explotionPrefab;
    public AudioClip audioFile;

    private ParticleSystem explosionParticle;
    private float currentHealth;
    private bool isDead = false;
    //private GameObject explotionPrefabClone;

    [SerializeField]
    private float animationTime;

    // Use this for initialization
    void Start ()
    {
        if(explotionPrefab)
        {
            animationTime = explotionPrefab.GetComponentInChildren<ParticleSystem>().duration;
            explotionPrefab.SetActive(false);
        }

        currentHealth = startHealth;
    }

    public void setDamage(float damage)
    {
        Debug.Log("[Weapon_Health] Im taking damaged: " + damage);
        currentHealth -= damage;

        if (currentHealth <= 0f && !isDead)
        {
            DoOnDeath();
        }
    }

    private void DoOnDeath()
    {
        isDead = true;

        //explotionPrefabClone.transform.position = transform.position;
        explotionPrefab.SetActive(true);

        // Destroy thisobject
        Destroy(this.gameObject, explotionPrefab.GetComponentInChildren<ParticleSystem>().duration);
    }
}
