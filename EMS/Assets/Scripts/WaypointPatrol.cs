using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    public Transform Concamera;
    private Transform targetObject;
    public Transform[] waypoints;
    GameObject target;
    GameObject targets;
    public int index;
    public float Zoomin = -5;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    Vector3 transformObject;
    int m_CurrentWaypointIndex;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            if (m_CurrentWaypointIndex == 9)
            {
                m_CurrentWaypointIndex = 0;
            }
            target = GameObject.Find("WayPoints").transform.GetChild(m_CurrentWaypointIndex++).gameObject;
            targets = GameObject.Find("Targets").transform.GetChild(index++).gameObject;
            Concamera.transform.position = target.transform.position;
            Concamera.transform.LookAt(targets.transform.position);
        }
    }
}
