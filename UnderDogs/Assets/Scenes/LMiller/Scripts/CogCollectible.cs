using UnityEngine;

public class CogCollectible : MonoBehaviour
{
    public int cogValue;
    public float cogSpinSpeed;

    public float cogVolume;

    private void Start()
    {
        GameManager.Instance.CountCogsInLevel();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime * cogSpinSpeed, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>()) 
        {
            
            GameManager.Instance.AddToCollection(cogValue);
            SoundManager.Play2DSound(SoundManager.Sound.CogCollected, cogVolume);
            gameObject.SetActive(false);
            
        }
    }
}
