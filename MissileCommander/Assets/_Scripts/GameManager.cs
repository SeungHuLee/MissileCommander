/* =============================================
 *  GameManager will work as composition root.
 *  Assigned Jobs :
 *     1. Dependency Entry Point
 *     2. Bind Events
 * =============================================*/

using System;
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

        private MouseGameController _mouseGameController;
        private BulletLauncher _launcher;
        private BuildingManager _buildingMgr;
        private MissileManager _missileMgr;
        private TimeManager _timeMgr;

        private void Start()
        {
            _timeMgr = gameObject.AddComponent<TimeManager>();
            
            _launcher = Instantiate(launcherPrefab);
            _launcher.transform.position = launcherLocator.position;
            
            _buildingMgr = new BuildingManager(buildingPrefab, buildingLocators, new Factory(destroyEffect, 2));

            _missileMgr = gameObject.AddComponent<MissileManager>();
            _missileMgr.Initialize(new Factory(missilePrefab), _buildingMgr, maxMissileCount, missileSpawnInterval);
            
            _mouseGameController = gameObject.AddComponent<MouseGameController>();
            
            BindEvents();
            
            _timeMgr.StartGame();
        }

        private void OnDestroy()
        {
            UnbindEvents();
        }

        private void BindEvents()
        {
            _mouseGameController.OnFireButtonPressed += _launcher.OnFireButtonPressed;
            _timeMgr.onGameStart += _buildingMgr.OnGameStart;
            _timeMgr.onGameStart += _launcher.OnGameStart;
            _timeMgr.onGameStart += _missileMgr.OnGameStart;
        }
        
        private void UnbindEvents()
        {
            _mouseGameController.OnFireButtonPressed -= _launcher.OnFireButtonPressed;
            _timeMgr.onGameStart -= _buildingMgr.OnGameStart;
            _timeMgr.onGameStart -= _launcher.OnGameStart;
            _timeMgr.onGameStart -= _missileMgr.OnGameStart;
        }
    }
}