using UnityEngine;

public class CogCollectible : MonoBehaviour
{
    public int cogValue;
    public float cogSpinSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, Time.deltaTime * cogSpinSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>()) 
        {
            //Increment UI Coin Value
            GameManager.Instance.AddToCollection(cogValue);
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
