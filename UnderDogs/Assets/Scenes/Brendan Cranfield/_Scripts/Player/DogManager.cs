using UnityEngine;

[AddComponentMenu("Player/Player Manager")]
public class DogManager : MonoBehaviour
{
    [Header("Health & Respawning")]
    [SerializeField] int maxHealth;
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
    }

    /// <summary> Explode creates an overlap sphere that grabs all the objects in it's radius, checks if it has a rigidbody and health and then does damage to the targets. </summary>
    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody)) { rigidbody.AddExplosionForce(power, transform.position, radius, 3, ForceMode.Impulse); }
            if (hit.TryGetComponent(out AIHealth aiHealth)) { aiHealth.healthSystem.Damage(attackDamage); }
        }
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
                break;

            case "collectable":
                GameManager.Instance.AddToCollection();
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