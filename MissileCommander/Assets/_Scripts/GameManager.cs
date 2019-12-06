/* =============================================
 *  GameManager will work as a composition root.
 *  Assigned Jobs :
 *     1. Dependency Entry Point
 *     2. Bind Events
 * =============================================*/

using System;
using System.Collections;
using UnityEngine;

namespace MissileCommander
{
    public class GameManager : MonoBehaviour
    {
        [Header("BulletLauncher")]
        [SerializeField] private BulletLauncher launcherPrefab;
        [SerializeField] private Transform launcherLocator;
        
        [Header("Building")]
        [SerializeField] private Building buildingPrefab;
        [SerializeField] private Transform[] buildingLocators;
        
        [Header("Missile & Effect")]
        [SerializeField] private Missile missilePrefab;
        [SerializeField] private DestroyEffect destroyEffect;
        [SerializeField] private float missileSpawnInterval = 0.5f;
        [SerializeField] private int maxMissileCount = 20;

        [Header("Scores")] 
        [SerializeField] private int scorePerMissile = 50;
        [SerializeField] private int scorePerBuilding = 5000;

        [Header("UI")] 
        [SerializeField] private UIRoot UIRoot;

        private bool _isAllBuildingDestroyed = false;

        private MouseGameController _mouseGameController;
        private BulletLauncher _launcher;
        private BuildingManager _buildingMgr;
        private MissileManager _missileMgr;
        private TimeManager _timeMgr;
        private ScoreManager _scoreMgr;

        /// <summary>
        /// Event to check :
        /// 1. whether the player won or lost
        /// 2. How many buildings left
        /// </summary>
        public event Action<bool, int> onGameOver;

        private void Start()
        {
            _timeMgr = gameObject.AddComponent<TimeManager>();
            
            _launcher = Instantiate(launcherPrefab);
            _launcher.transform.position = launcherLocator.position;
            
            _buildingMgr = new BuildingManager(buildingPrefab, buildingLocators, new Factory(destroyEffect, 2));

            _missileMgr = gameObject.AddComponent<MissileManager>();
            _missileMgr.Initialize(new Factory(missilePrefab), _buildingMgr, maxMissileCount, missileSpawnInterval);
            
            _scoreMgr = new ScoreManager(scorePerBuilding, scorePerMissile);
            
            _mouseGameController = gameObject.AddComponent<MouseGameController>();
            
            BindEvents();
            
            _timeMgr.StartGame();
        }

        private void OnAllBuildingDestroyed()
        {
            _isAllBuildingDestroyed = true;
            AudioManager.Instance.PlaySound(SoundID.GameOver);
            onGameOver?.Invoke(false, _buildingMgr.BuildingCount);
        }

        private void OnAllMissileReturned()
        {
            Debug.Log("YOU WIN!!!");
            StartCoroutine(CheckGameOver());
        }

        private IEnumerator CheckGameOver()
        {
            yield return null;

            if (!_isAllBuildingDestroyed)
            {
                AudioManager.Instance.PlaySound(SoundID.GameOver);
                onGameOver?.Invoke(true, _buildingMgr.BuildingCount);
            }
        }

        private void OnDestroy()
        {
            UnbindEvents();
        }

        private void BindEvents()
        {
            _timeMgr.onGameStart += _buildingMgr.OnGameStart;
            _timeMgr.onGameStart += _launcher.OnGameStart;
            _timeMgr.onGameStart += _missileMgr.OnGameStart;
            _timeMgr.onGameStart += UIRoot.OnGameStart;
            
            _mouseGameController.OnFireButtonPressed += _launcher.OnFireButtonPressed;
            _scoreMgr.onScoreChanged += UIRoot.OnScoreChanged;
            _missileMgr.onMissileDestroyed += _scoreMgr.OnMissileDestroyed;
            _buildingMgr.onAllBuildingDestroyed += OnAllBuildingDestroyed;
            _missileMgr.onAllMissileReturned += OnAllMissileReturned;

            this.onGameOver += _launcher.OnGameOver;
            this.onGameOver += _missileMgr.OnGameOver;
            this.onGameOver += _scoreMgr.OnGameOver;
            this.onGameOver += UIRoot.OnGameOver;
        }
        
        private void UnbindEvents()
        {
            _timeMgr.onGameStart -= _buildingMgr.OnGameStart;
            _timeMgr.onGameStart -= _launcher.OnGameStart;
            _timeMgr.onGameStart -= _missileMgr.OnGameStart;
            _timeMgr.onGameStart -= UIRoot.OnGameStart;
            
            _mouseGameController.OnFireButtonPressed -= _launcher.OnFireButtonPressed;
            _scoreMgr.onScoreChanged -= UIRoot.OnScoreChanged;
            _missileMgr.onMissileDestroyed -= _scoreMgr.OnMissileDestroyed;
            _buildingMgr.onAllBuildingDestroyed -= OnAllBuildingDestroyed;
            _missileMgr.onAllMissileReturned -= OnAllMissileReturned;
            
            this.onGameOver += _launcher.OnGameOver;
            this.onGameOver += _missileMgr.OnGameOver;
            this.onGameOver += _scoreMgr.OnGameOver;
            this.onGameOver += UIRoot.OnGameOver;
        }
    }
}