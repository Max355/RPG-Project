using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.SceneManagement;
using RPG.Control;
using Cysharp.Threading.Tasks;


namespace RPG.Scenemanagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;
        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player")
            {
                //StartCoroutine(Transition());
                Transition().Forget();
            }
        }
        // private IEnumerator Transition()
        // {
        //     if (sceneToLoad < 0)
        //     {
        //         Debug.LogError("Scene to load not set.");
        //         yield break;
        //     }
        //     DontDestroyOnLoad(gameObject);

        //     Fader fader = FindObjectOfType<Fader>();
        //     SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        //     PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //     playerController.enabled = false;

        //     yield return fader.FadeOut(fadeOutTime);

        //     savingWrapper.Save();

        //     yield return SceneManager.LoadSceneAsync(sceneToLoad);
        //     PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //     newPlayerController.enabled = false;


        //     savingWrapper.Load();

        //     Portal otherPortal = GetOtherPortal();
        //     UpdatePlayer(otherPortal);
        //     savingWrapper.Save();

        //     yield return new WaitForSeconds(fadeWaitTime);
        //     fader.FadeIn(fadeInTime);

        //     newPlayerController.enabled = true;
        //     Destroy(gameObject);
        // }

         private async UniTaskVoid Transition()
    {
        if (sceneToLoad < 0)
        {
            Debug.LogError("Scene to load not set.");
            return;
        }
        DontDestroyOnLoad(gameObject);

        Fader fader = FindObjectOfType<Fader>();
        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.enabled = false;

        await fader.FadeOut(fadeOutTime);

        savingWrapper.Save();

        await SceneManager.LoadSceneAsync(sceneToLoad);
        PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        newPlayerController.enabled = false;

        savingWrapper.Load();

        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);
        savingWrapper.Save();

        await UniTask.Delay((int)(fadeWaitTime * 1000));

        await fader.FadeIn(fadeInTime);

        newPlayerController.enabled = true;
        Destroy(gameObject);
    }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
        // enum DestinationIdentifier
        // {
        //     A, B
        // }
        // [SerializeField] int sceneToLoad = -1;
        // [SerializeField] Transform spawnPoint;
        // [SerializeField] DestinationIdentifier destination;
        // [SerializeField] float fadeOutTime = 1f;
        // [SerializeField] float fadeInTime = 2f;
        // [SerializeField] float fadeWaitTime = 0.5f;
        
        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.tag == "Player")
        //     {
        //         StartCoroutine(Transition());
        //     }
        // }

        // private IEnumerator Transition()
        // {
            
            
        //     DontDestroyOnLoad(gameObject);

        //     Fader fader = FindObjectOfType<Fader>();
        //     SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
        //     PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //     playerController.enabled = false;
            
        //     yield return fader.FadeOut(fadeOutTime);
            
        //     wrapper.Save();

        //     yield return SceneManager.LoadSceneAsync(sceneToLoad);
        //     PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        //     newPlayerController.enabled = false;

        //     //Load current level
        //     wrapper.Load();
            
        //     Portal otherPortal = GetOtherPortal();
        //     UpdatePlayer(otherPortal);

        //     wrapper.Save();

        //     yield return new WaitForSeconds(fadeWaitTime);
        //     fader.FadeIn(fadeInTime);

        //     newPlayerController.enabled = true;
        //     Destroy(gameObject);
        // }

        // private T FindAnyObjectOfType<T>()
        // {
        //     throw new NotImplementedException();
        // }

        // private void UpdatePlayer(Portal otherPortal)
        // {
        //     GameObject player = GameObject.FindWithTag("Player");
        //     player.GetComponent<NavMeshAgent>().enabled = false;
        //     player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        //     player.transform.rotation = otherPortal.spawnPoint.rotation;
        //     player.GetComponent<NavMeshAgent>().enabled = true;
            
            
        // }

        // private Portal GetOtherPortal()
        // {
        //     foreach (Portal portal in FindObjectsOfType<Portal>())
        //     {
        //         if (portal == this) continue;
        //         if (portal.destination != destination) continue;//allow us make different destinations in spawn point

        //         return portal;
        //     }
        //     return null;
        // }
    }
}

