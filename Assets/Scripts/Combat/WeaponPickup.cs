using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RPG.Control;
using RPG.Movement;
using UnityEngine;


namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon = null;
        [SerializeField] float respawnTime = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Pickup(other.GetComponent<Fighter>());
            }
        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            HideForSeconds(respawnTime).Forget(); // Forget() suppresses any unhandled exception warnings
            //StartCoroutine(HideForSeconds(respawnTime));
        }

        // private IEnumerator HideForSeconds(float seconds)
        // {
        //     ShowPickup(false);
        //     yield return new WaitForSeconds(seconds);
        //     ShowPickup(true);
        // }

        private async UniTaskVoid HideForSeconds(float seconds)
        {
            ShowPickup(false);
            await UniTask.Delay((int)(seconds * 1000)); // Delay for specified seconds
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButton(0))
            {
                //Pickup(callingController.GetComponent<Fighter>());
                callingController.GetComponent<Mover>().StartMoveAction(transform.position, 1f);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }   
}
