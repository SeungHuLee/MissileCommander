using System;
using System.Collections;
using UnityEngine;

namespace MissileCommander
{
    public class TimeManager : MonoBehaviour
    {
        public event Action onGameStart;
        private bool _isGameStarted = false;

        public void StartGame(float startDelay = 1f)
        {
            if (_isGameStarted) { return; }

            _isGameStarted = true;
            StartCoroutine(DelayedGameStart(startDelay));
        }

        private IEnumerator DelayedGameStart(float delayTime)
        {
            WaitForSeconds delay = new WaitForSeconds(delayTime);
            yield return delay;
            
            onGameStart?.Invoke();
        }
    }
}