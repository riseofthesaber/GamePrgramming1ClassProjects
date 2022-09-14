using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{   

    [SerializeField] 
    
    private float speed = 2.0f;

    private List<Vector3> places = new List<Vector3> (){new Vector3(0,0,0),new Vector3(-5, 0,0)};

    private int pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
         gameObject.transform.position= Vector3.MoveTowards(gameObject.transform.position,places[pos],speed*Time.deltaTime);
        if(places[pos].x == gameObject.transform.position.x){
            if(pos>=(places.Count - 1)){
                pos = 0;
            }else{
                pos += 1;
            }
        }
        Debug.Log(pos);
    }
}
