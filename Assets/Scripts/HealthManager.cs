using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public float startHealth = 100f;
    public float damageTreshHold = 50f;
    public Slider slider;
    public Image fillImage;
    public Color maxHealthColor = Color.green;
    public Color minHealthColor = Color.red;
    public GameObject explotionPrefab;
    public AudioClip audioFile;
    public LayerMask groundLayer;
   

    //Donotchange these#private AudioSource audioFile;
    //private GameObject
    private ParticleSystem explosionParticle;
    private ParticleSystem damageParticle;
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
        //if (currentHealth <= damageTreshHold)
        //{
        //    GetComponentInChildren<ParticleSystem>().enableEmission = true;
        //}
        //else
        //{
        //    GetComponentInChildren<ParticleSystem>().enableEmission = false;
        //}
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
        //Debug.Log("[HeakthManager] Im taking damaged: " + damage);
        currentHealth -= damage;

        SetHealthBarUI();
        if(currentHealth <= damageTreshHold)
        {
            GetComponentInChildren<ParticleSystem>().enableEmission = true;
        }

        if (currentHealth <= 0f && !isDead)
        {
            DoOnDeath2();
        }
    }

    /// <summary>
    /// Destroy this gameobject
    /// </summary>
    private void DoOnDeath1()
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
    
    private void DoOnDeath2()
    {
        isDead = true;

        GetComponentInParent<MoveOverLocalPath>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        if(GetComponent<Animator>())
        {
            GetComponent<Animator>().Stop();
            GetComponent<Animator>().enabled = false;
        }
    }

    //void OnTriggerEnter(Collider collidedObject)
    void OnCollisionEnter(Collision collidedObject)
    {
        if (!isDead)
            return;

        Debug.Log("Detecting hit: objectname: " + collidedObject.gameObject.name + " On layer: " + collidedObject.gameObject.layer);
        float layerID = Mathf.Log(groundLayer.value, 2);

       // Debug.Log("comparing: " + collidedObject.gameObject.layer + " to " + layerID);
        //if (collidedObject.gameObject.layer == layerID)
        //{
            //Debug.Log("i hit: " + collidedObject.name);
            Debug.Log("i hit: " + collidedObject.gameObject.name);

            explotionPrefab.transform.position = transform.position;
            explotionPrefab.SetActive(true);

            // Remove explostion prefab when its done animating
            Destroy(explotionPrefab, explosionParticle.duration);

            //Destroy thisobject
            Destroy(this.gameObject);
        //}
    }

    void SetHealthBarUI()
    {
        if (slider)
        {
            //Debug.Log("[Healthmanager] health circle at: " + currentHealth / startHealth + " %");
            slider.value = currentHealth;
            fillImage.color = Color.Lerp(minHealthColor, maxHealthColor, currentHealth / startHealth);
        }
        else
        {
            Debug.Log("[Healthmanager] Cannot display health Please select the GUI slider prefab first");
        }
    }

}
