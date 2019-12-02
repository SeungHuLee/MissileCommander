using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MissileCommander
{
    public class MissileManager : MonoBehaviour
    {
        [SerializeField] private Missile missilePrefab;
        private Factory _missileFactory;
        private BuildingManager _buildingMgr;

        private bool _isInitialized = false;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main != null ? Camera.main : FindObjectOfType<Camera>();
        }

        public void Initialize(Factory missileFactory, BuildingManager buildingMgr)
        {
            if (_isInitialized) { return; }
            
            this._missileFactory = missileFactory;
            this._buildingMgr = buildingMgr;
            
            Debug.Assert(_missileFactory != null, "MissileManager : Missile Factory is null!");
            Debug.Assert(_buildingMgr != null, "MissileManager : BuildingManager is null!");

            _isInitialized = true;
        }

        public void OnGameStart()
        {
            SpawnMissile();
        }

        private void SpawnMissile()
        {
            Debug.Assert(_missileFactory != null, "MissileManager : Missile Factory is null!");
            Debug.Assert(_buildingMgr != null, "MissileManager : BuildingManager is null!");
            
            RecyclableObject missile = _missileFactory.Get();
            missile.Activate(GetMissileSpawnPosition(), _buildingMgr.GetRandomBuildingPosition());

            missile.onDestroyed += this.OnMissileDestroyed;
        }

        private Vector3 GetMissileSpawnPosition()
        {
            Vector3 spawnPosition = Vector3.zero;
            spawnPosition.x = Random.Range(0f, 1f);
            spawnPosition.y = 1f;
            
            spawnPosition = _mainCamera.ViewportToWorldPoint(spawnPosition);
            spawnPosition.z = 0f;
            return spawnPosition;
        }

        private void OnMissileDestroyed(RecyclableObject missile)
        {
            missile.onDestroyed -= this.OnMissileDestroyed;
            _missileFactory.ReturnToPool(missile);
        }
    }
}