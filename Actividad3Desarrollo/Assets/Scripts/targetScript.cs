using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    [SerializeField]
    private int pointsToAdd;
    [SerializeField]
    private GameObject scoreManager;

    public string targetTag;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("scoreManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendPoints()
    {
        scoreManager.GetComponent<scoreManager>().addPoints(pointsToAdd);
    }
}
