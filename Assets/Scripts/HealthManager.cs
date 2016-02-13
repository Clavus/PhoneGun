using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public float startHealth = 100f;
    public Slider slider;
    public Image fillImage;
    public Color maxHealthColor = Color.green;
    public Color minHealthColor = Color.red;
    public GameObject explotionPrefab;
    public AudioClip audioFile;
   

    //Donotchange these#private AudioSource audioFile;
    //private GameObject
    private ParticleSystem explosionParticle;
    public float currentHealth;
    private bool isDead;
    private float destroyTimer;
    private AudioSource audioSource;


    void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        setSliderStartValue();
        explotionPrefab = Instantiate(this.explotionPrefab);
        explosionParticle = explotionPrefab.GetComponentInChildren<ParticleSystem>();
        explotionPrefab.SetActive(false);
    }


    // Update is called once per frame
    void Update ()
    {
	
	}

    void setSliderStartValue()
    {
        if (slider)
        {
            slider.maxValue = startHealth;
            SetHealthBarUI();
        }
    }


    public void setDamage(float damage)
    {
        Debug.Log("[HeakthManager] Im taking damaged: " + damage);
        currentHealth -= damage;

        SetHealthBarUI();

        if (currentHealth <= 0f && !isDead)
        {
            DoOnDeath();
        }
    }

    /// <summary>
    /// Destroy this gameobject
    /// </summary>
    private void DoOnDeath()
    {
        isDead = true;

        // Set explostion refab to players posistion and enabled it
        explotionPrefab.transform.position = transform.position;
        Debug.Log("[Healthmanager] Spawning explosion prefab to player loc: " + explotionPrefab.name);
        explotionPrefab.SetActive(true);

        //Destroy thisobject
        Destroy(this.gameObject);

        // Remove explostion prefab when its done animating
        Destroy(explotionPrefab, explosionParticle.duration);
    }

    void SetHealthBarUI()
    {
        if (slider)
        {
            Debug.Log("[Healthmanager] health circle at: " + currentHealth / startHealth + " %");
            slider.value = currentHealth;
            fillImage.color = Color.Lerp(minHealthColor, maxHealthColor, currentHealth / startHealth);
        }
        else
        {
            Debug.Log("[Healthmanager] Cannot display health Please select the GUI slider prefab first");
        }
    }

}
