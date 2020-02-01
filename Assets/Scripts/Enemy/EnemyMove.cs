using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed=3f;
    public bool moveUp=true;
    public bool moveDown=true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
          MoveUpDown();


    }


    public void MoveUpDown()
    {


      if (moveUp)
      {
      transform.Translate(transform.up * Time.deltaTime * speed);

      transform.position= new Vector2(transform.position.x,transform.position.y);


    };



    if(transform.position.y>=4)
    {
      transform.position= new Vector2(transform.position.x,transform.position.y);


      moveUp=false;
      moveDown=true;



    }
    if(!moveUp)
    {

      transform.Translate(-transform.up * Time.deltaTime * speed);

    }

    if(transform.position.y<=-4)
    {
      transform.position= new Vector2(transform.position.x,transform.position.y);

      moveDown=false;
      moveUp=true;


    }
    if(!moveDown)
    {

      transform.Translate(transform.up * Time.deltaTime * speed);

    }













    }



}
