using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float lifesPan;
    [SerializeField]
    private bool isSticky, isGravity;
    [SerializeField]
    private Transform anchoredObject;
    [SerializeField]
    private Vector3 objectPos, offsetPos;

    public Transform magnet;
    public float speed;
    public float magnetDistance;
    public Collider[] hitColliders;

    public string targetTag;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyBullet());
        magnet = gameObject.transform;
        speed = 10;
        magnetDistance = 2;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isGravity)
        {
            hitColliders = Physics.OverlapSphere(magnet.position, magnetDistance);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "attractable")
                {
                    float distance = Vector3.Distance(hitColliders[i].transform.position, gameObject.transform.position);

                    if (distance > 1)
                    {
                        Transform attract = hitColliders[i].transform;
                        attract.position = Vector3.MoveTowards(attract.position, magnet.position, speed * Time.deltaTime);
                    }
                    else
                    {
                        Transform attract = hitColliders[i].transform;
                        attract.RotateAround(gameObject.transform.position, Vector3.up, 80 * Time.deltaTime);

                    }

                }
            }
        }

    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(lifesPan);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isSticky && gameObject.GetComponent<Rigidbody>())
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            gameObject.transform.SetParent(collision.gameObject.transform);
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

        if (collision.gameObject.GetComponent<targetScript>())
        {
            if (collision.gameObject.GetComponent<targetScript>().targetTag == targetTag)
            {
                collision.gameObject.GetComponent<targetScript>().sendPoints();
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        
    }

    
}
