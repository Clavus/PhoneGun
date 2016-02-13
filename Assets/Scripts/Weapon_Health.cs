using UnityEngine;
using System.Collections;

public class Weapon_Health : MonoBehaviour {

    public float startHealth = 10f;
    public GameObject explotionPrefab;
    public AudioClip audioFile;

    private ParticleSystem explosionParticle;
    private float currentHealth;
    private bool isDead = false;

    // Use this for initialization
    void Start ()
    {
        if(explotionPrefab)
        {
            explotionPrefab = Instantiate(this.explotionPrefab);
            explosionParticle = explotionPrefab.GetComponentInChildren<ParticleSystem>();
            explotionPrefab.SetActive(false);
        }

        currentHealth = startHealth;
    }

    public void setDamage(float damage)
    {
        //Debug.Log("[HeakthManager] Im taking damaged: " + damage);
        currentHealth -= damage;

        if (currentHealth <= 0f && !isDead)
        {
            DoOnDeath();
        }
    }

    private void DoOnDeath()
    {
        isDead = true;

        // Set explostion refab to players posistion and enabled it
        explotionPrefab.transform.position = transform.position;
        explotionPrefab.SetActive(true);

        //Destroy thisobject
        Destroy(this.gameObject);

        // Remove explostion prefab when its done animating
        Destroy(explotionPrefab, explosionParticle.duration);
    }
}
