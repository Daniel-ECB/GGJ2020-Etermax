using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject gameMenu; 
    // Start is called before the first frame update
    void Awake()
    {
        if(gameManager==null)
        {

            gameManager=this;


        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
