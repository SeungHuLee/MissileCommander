using System;
using UnityEngine;
using TMPro;

namespace MissileCommander
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Awake()
        {
            scoreText.gameObject.SetActive(false);
        }

        public void OnGameStart()
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = $"Score : { 0 }";
        }

        public void OnScoreChanged(int score)
        {
            scoreText.text = $"Score : { score }";
        }
    }
}