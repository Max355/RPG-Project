using System.Collections;
using Cysharp.Threading.Tasks;
using RPG.Saving;
using UnityEngine;
using System.Threading.Tasks;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 0.2f;
        
        private void Awake() 
        {
           StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene() {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();

            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }


        private void Update() {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }
        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }

        

        // [SerializeField] KeyCode saveKey = KeyCode.S;
        // [SerializeField] KeyCode loadKey = KeyCode.L;
        // [SerializeField] KeyCode deleteKey = KeyCode.Delete;
        // const string defaultSaveFile = "save";
        
        // private void Awake() 
        // {
        //     StartCoroutine(LoadLastScene());
        // }

        // private IEnumerator LoadLastScene() {
        //     yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        // }

        // private void Update() {
        //     if (Input.GetKeyDown(saveKey))
        //     {
        //         Save();
        //     }
        //     if (Input.GetKeyDown(loadKey))
        //     {
        //         Load();
        //     }
        //     if (Input.GetKeyDown(deleteKey))
        //     {
        //         Delete();
        //     }
        // }

        // public void Load()
        // {
        //     StartCoroutine(GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile));
        // }

        // public void Save()
        // {
        //     GetComponent<SavingSystem>().Save(defaultSaveFile);
        // }

        // public void Delete()
        // {
        //     GetComponent<SavingSystem>().Delete(defaultSaveFile);
        // }
    }
}