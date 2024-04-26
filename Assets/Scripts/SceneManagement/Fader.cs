using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    { 
        CanvasGroup canvasGroup;
        // Coroutine currentActiveFade = null;
        CancellationTokenSource cts = new CancellationTokenSource();
        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        // public Coroutine FadeOut(float time)
        // {
        //     return Fade(1, time);
        // }

        public UniTask FadeOut(float time)
        {
            return Fade(1, time);
        }
        // public Coroutine FadeIn(float time)
        // {
        //     return Fade(0, time);
        // }

        public UniTask FadeIn(float time)
        {
            return Fade(0, time);
        }

        // public Coroutine Fade(float target, float time)
        // {
        //     if (currentActiveFade != null)
        //     {
        //         StopCoroutine(currentActiveFade);
        //     }
        //     currentActiveFade = StartCoroutine(FadeRoutine(target, time));
        //     return currentActiveFade;
        // }

        public async UniTask Fade(float target, float time)
    {
        cts.Cancel(); // Cancel the previous task
        cts = new CancellationTokenSource(); // Create a new CancellationTokenSource

        await FadeRoutine(target, time, cts.Token);
    }

        // private IEnumerator FadeRoutine(float target, float time)
        // {
        //     while (!Mathf.Approximately(canvasGroup.alpha, target))
        //     {
        //         canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
        //         yield return null;
        //     }
        // }

        private async UniTask FadeRoutine(float target, float time, CancellationToken token)
    {
        while (!Mathf.Approximately(canvasGroup.alpha, target))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }
        // CanvasGroup canvasGroup;
        // Coroutine currentActiveFade = null;
        // private void Awake()
        // {
        //    canvasGroup = GetComponent<CanvasGroup>();
        // }  

        //  public void FadeOutImmediate()
        // {
        //     canvasGroup.alpha = 1;
        // }

        // public Coroutine FadeOut(float time)
        // {
        //     //Canvas running coroutines
        //     return Fade(1, time);
        //     //Run fadeout coroutines
            
        // }

        // private Coroutine Fade(float target, float time)
        // {
        //     if (currentActiveFade != null)
        //     {
        //         StopCoroutine(currentActiveFade);
        //     }
        //     currentActiveFade = StartCoroutine(FadeRoutine(target, time));
        //     return currentActiveFade;
        // }

        // public Coroutine FadeIn(float time)
        // {
        //     //Canvas running coroutines
        //     return Fade(0, time);
        //     //Run fadeout coroutines
        // }

        // private IEnumerator FadeRoutine(float target, float time)
        // {
        //     while (!Mathf.Approximately(canvasGroup.alpha, target))
        //     {
        //         canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
        //         yield return null;
        //     }
        // }

        
    
    }
}

