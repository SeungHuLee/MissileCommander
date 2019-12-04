using UnityEngine;
using System;

namespace MissileCommander
{
    public class ScoreManager
    {
        private readonly int _scorePerBuilding;
        private readonly int _scorePerMissile;

        private int _score;

        public event Action<int> onScoreChanged;

        public ScoreManager(int scorePerBuilding = 5000, int scorePerMissile = 50)
        {
            this._scorePerBuilding = scorePerBuilding;
            this._scorePerMissile = scorePerMissile;
        }

        public void OnMissileDestroyed()
        {
            _score += _scorePerMissile;
            onScoreChanged?.Invoke(_score);
        }

        public void OnGameOver()
        {
            
        }
    }
}