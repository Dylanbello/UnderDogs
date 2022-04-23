using System.Collections;
using System.Collections.Generic;
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
    Animator animator;

    [Header("Pickup")]
    [SerializeField] Transform currentPickedUp;
    [HideInInspector] public bool pickupFlag;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();//Gets dog animator
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
        StartCoroutine(ded());
        
        ResetHealth();
    }
    
    IEnumerator ded()
    { 
        charController.enabled = false;
        animator.SetTrigger("Die");
        Debug.Log(animator.GetBool("Die"));
        yield return new WaitForSeconds(5); 

        transform.position = spawnPoint;
        charController.enabled = true;
    }

    /// <summary> Explode creates an overlap sphere that grabs all the objects in it's radius, checks if it has a rigidbody and health and then does damage to the targets. </summary>
    public void Explode()
    {
        animator.SetTrigger("GetUp");
        animator.CrossFade("SpinAttack",0);//Get spin attack in animator
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        for(int i=0;i<colliders.Length;i++)
        {
            Rigidbody rigidbody = colliders[i].GetComponent<Rigidbody>();
            AIHealth aiHealth = colliders[i].GetComponent<AIHealth>();

            if (rigidbody) { rigidbody.AddExplosionForce(power, transform.position, radius, 3, ForceMode.Impulse); }
            if (aiHealth) { aiHealth.healthSystem.Damage(attackDamage); }
        }
    }

    void ResetHealth()
    {
        int giveHealthAmount = maxHealth - playerHealth.GetHealth();
        playerHealth.Heal(giveHealthAmount);
        animator.ResetTrigger("Die");
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