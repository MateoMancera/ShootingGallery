using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatScript : MonoBehaviour
{
    private Vector3 originalPos;
    public float hoverHeight, hoverRange, hoverSpeed, maxHeight, minimumHeight;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        hoverHeight = (maxHeight + minimumHeight) / 2.0f;
        hoverRange = maxHeight - minimumHeight;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.up * (hoverHeight + minimumHeight*Mathf.Cos(Time.time * hoverSpeed) * hoverRange);
        this.transform.position = new Vector3(originalPos.x, this.transform.position.y, originalPos.z);
    }
}
