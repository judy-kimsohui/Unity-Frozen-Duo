using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour
{
    public GameObject wall;
    public Animator wallAnim;

    void OnTriggerEnter(Collider other)
    {
        int currentValue = wallAnim.GetInteger("OnWayPoints");
        wallAnim.SetInteger("OnWayPoints", currentValue + 1);
    }

    void OnTriggerExit(Collider other)
    {
        int currentValue = wallAnim.GetInteger("OnWayPoints");
        wallAnim.SetInteger("OnWayPoints", currentValue - 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.Find("Falling Wall");
        wallAnim = wall.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
