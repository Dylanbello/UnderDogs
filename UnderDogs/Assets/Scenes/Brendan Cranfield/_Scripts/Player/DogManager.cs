using UnityEngine;

[AddComponentMenu("Player/Player Manager")]
public class DogManager : MonoBehaviour
{
    [Header("Health & Respawning")]
    [SerializeField] int maxHealth = 100;
    public HealthSystem playerHealth;
    Vector3 spawnPoint;
    CharacterController charController;

    [Header("Attack values")]
    [SerializeField] float power = 1;
    [SerializeField] int attackDamage = 20;
    [SerializeField] float radius = 1;

    [Header("Pickup")]
    [SerializeField] Transform currentPickedUp;
    [HideInInspector] public bool pickupFlag;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        playerHealth = new HealthSystem(maxHealth);     //Inputs the maximum health for the player.
        spawnPoint = transform.position;                //Sets starting checkpoint.

        playerHealth.OnDead += PlayerHealth_OnDead;
        playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
    }

    private void PlayerHealth_OnHealthChanged(object sender, System.EventArgs e)
    {
        Debug.Log(playerHealth.GetHealth());
    }

    private void PlayerHealth_OnDead(object sender, System.EventArgs e)
    {
        Debug.Log($"{gameObject.name} is dead");
        charController.enabled = false;
        transform.position = spawnPoint;
        charController.enabled = true;
        ResetHealth();
    }

    /// <summary> Explode creates an overlap sphere that grabs all the objects in it's radius, checks if it has a rigidbody and health and then does damage to the targets. </summary>
    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();
            AIHealth aiHealth = hit.GetComponent<AIHealth>();

            if (rigidbody) { rigidbody.AddExplosionForce(power, transform.position, radius, 3, ForceMode.Impulse); }
            if (aiHealth) { aiHealth.healthSystem.Damage(attackDamage); }
        }
    }

    void ResetHealth()
    {
        int giveHealthAmount = maxHealth - playerHealth.GetHealth();
        playerHealth.Heal(giveHealthAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)   //Can be used for interacting with objects.
        {
            case "checkpoint":
                spawnPoint = other.transform.position;
                break;

            case "respawn":
                charController.enabled = false;
                transform.position = spawnPoint;
                charController.enabled = true;
                ResetHealth();
                
                break;

            case "pickupable":
                
                break;
        }
    }

    void PickupObject()
    {

    }

    void DropObject()
    {

    }

    void Interact()
    {

    }
}