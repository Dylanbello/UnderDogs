using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game script reference from https://answers.unity.com/questions/12083/how-to-get-a-character-to-move-with-a-moving-platf.html by user Mahtub
public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject target = null;
    private Vector3 offset;
    public float moveDistance;
    public float moveSpeed;
    public bool detected;
    public GameObject destination;
    public GameObject origin;
    private List<Collider> TriggerList = new List<Collider>();
    void Start()
    {
        target = null;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }
    //public enum Axis { X_AXIS, Y_AXIS, Z_AXIS }

    //public Axis axis;

    void Update()
    {
        if(detected == true)
        {
            StartCoroutine(DelayElevator(destination.transform.position));
            //transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, (8.5f * Time.deltaTime));
        }
        if(detected == false)
        {
            if(this.transform.position.y > 11.43)
            {
                StartCoroutine(DelayElevator(origin.transform.position));
                //transform.position = Vector3.MoveTowards(transform.position, origin.transform.position, (8.5f * Time.deltaTime));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = this.transform;
        TriggerList.Add(other);
        if(TriggerList.Count == 2)
        {
            detected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
        TriggerList.Remove(other);
        if (TriggerList.Count == 0)
        {
            detected = false;
        }
    }

    private IEnumerator DelayElevator(Vector3 direction)
    {
        yield return new WaitForSeconds(1);
        transform.position = Vector3.MoveTowards(transform.position, direction, (8.5f * Time.deltaTime));
    }
}
