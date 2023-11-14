using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public float forceMultiflier = 25.0f;
    public Vector3 windDirection;
    public Rigidbody forcedrb;      //rigidbody of forced object

    void OnTriggerStay(Collider other)
    { 
        if(other.gameObject.CompareTag("Player1")); //바람과 겹쳐있는 게임 오브젝트가 Player1이라면

        {
            forcedrb = other.attachedRigidbody;  //get rigidbody from triggering collider
		    forcedrb.AddForce(windDirection * forceMultiflier, ForceMode.Force);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
	    windDirection = transform.up; //get y direction of wind
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
