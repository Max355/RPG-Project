using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Resources;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
      [SerializeField] Transform target;
      [SerializeField] float maxSpeed = 6f;
      NavMeshAgent navMeshAgent;
      Health health;

      private void Start()
      {
         navMeshAgent = GetComponent<NavMeshAgent>();
         health = GetComponent<Health>();
      }

      void Update()
      {
         navMeshAgent.enabled = !health.IsDead();//disable character navmesh so our player can go throught without stoping
         UpdateAnimator();
      }

      public void StartMoveAction(Vector3 destination, float speedFraction)
      {
         GetComponent<ActionScheduler>().StartAction(this);
         MoveTo(destination, speedFraction);
      }

      public void MoveTo(Vector3 destination, float speedFraction)
      {
         navMeshAgent.destination = destination;
         navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
         navMeshAgent.isStopped = false;
      }

      public void Cancel()
      {
         navMeshAgent.isStopped = true;
      }

      

      private void UpdateAnimator()
      {
         Vector3 velocity = navMeshAgent.velocity;//Get the global velocity from Nav Mesh Agent
         Vector3 localVelocity = transform.InverseTransformDirection(velocity);//Convert this into a local value relative to the character
         float speed = localVelocity.z;
         GetComponent<Animator>().SetFloat("forwardSpeed", speed);//Set the Animator's blend value to be equel to our desired forward speed on z axis
      }
   }
}
