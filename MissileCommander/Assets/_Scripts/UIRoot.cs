using System;
using UnityEngine;
using TMPro;

namespace MissileCommander
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI resultText;

        private void Awake()
        {
            scoreText.gameObject.SetActive(false);
            resultText.gameObject.SetActive(false);
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
        
        public void OnGameOver(bool isVictory, int buildingCount)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = isVictory ? "YOU WIN!!!" : "YOU LOSE!!!";
        }
    }
}