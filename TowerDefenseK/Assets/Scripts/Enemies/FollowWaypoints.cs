using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class FollowWaypoints : MonoBehaviour
    {
        [SerializeField] private List<Transform> waypoints;
        public GameObject waypointHolder;
        public float speed;
        public float minDistance; 
        public int currentWaypoint = 0;

        private void Start()
        {
            waypointHolder = GameObject.Find("WaypointsHolder");
            for (var indexWaypoint = 0; indexWaypoint < waypointHolder.transform.childCount; indexWaypoint++)
            {
                waypoints.Add(waypointHolder.transform.GetChild(indexWaypoint));
            }
        }

        private void Update()
        {
            var target = waypoints[currentWaypoint];
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) < minDistance)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Count)
                {
                    currentWaypoint = 0;
                }
            }
        }
    }
}
