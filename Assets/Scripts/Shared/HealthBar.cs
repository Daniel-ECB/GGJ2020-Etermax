using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float maxHits = 100.0f;
    private float currentHits = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true) {
            if (currentHits>0)
            currentHits -= 5.0f;
            transform.localScale = new Vector3(currentHits / maxHits, 1.0f, 1.0f);
        
        }
    }
}
