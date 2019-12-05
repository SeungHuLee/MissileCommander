using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MissileCommander
{
    public class MissileManager : MonoBehaviour
    {
        private Factory _missileFactory;
        private BuildingManager _buildingMgr;

        private bool _isInitialized = false;
        private int _maxMissileCount = 20;
        private float _missileSpawnInterval = 0.5f;
        private int _currentMissileCount;

        private Coroutine _spawningMissile;

        private Camera _mainCamera;

        private List<RecyclableObject> _missiles = new List<RecyclableObject>();

        public event Action onMissileDestroyed;

        private void Awake()
        {
            _mainCamera = Camera.main != null ? Camera.main : FindObjectOfType<Camera>();
        }

        public void Initialize(Factory missileFactory, BuildingManager buildingMgr, int maxMissileCount,
            float missileSpawnInterval)
        {
            if (_isInitialized)
            {
                return;
            }

            this._missileFactory = missileFactory;
            this._buildingMgr = buildingMgr;
            this._maxMissileCount = maxMissileCount;
            this._missileSpawnInterval = missileSpawnInterval;

            Debug.Assert(_missileFactory != null, "MissileManager : Missile Factory is null!");
            Debug.Assert(_buildingMgr != null, "MissileManager : BuildingManager is null!");

            _isInitialized = true;
        }

        public void OnGameStart()
        {
            _currentMissileCount = 0;
            _spawningMissile = StartCoroutine(AutoSpawnMissile());
        }

        private void SpawnMissile()
        {
            Debug.Assert(_missileFactory != null, "MissileManager : Missile Factory is null!");
            Debug.Assert(_buildingMgr != null, "MissileManager : BuildingManager is null!");

            RecyclableObject missile = _missileFactory.Get();
            missile.Activate(GetMissileSpawnPosition(), _buildingMgr.GetRandomBuildingPosition());

            missile.onDestroyed += this.OnMissileDestroyed;
            missile.onOutOfScreen += this.OnMissileOutOfScreen;
            _missiles.Add(missile);

            _currentMissileCount++;
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

        private IEnumerator AutoSpawnMissile()
        {
            WaitForSeconds spawnInterval = new WaitForSeconds(_missileSpawnInterval);
            while (_currentMissileCount < _maxMissileCount)
            {
                yield return spawnInterval;

                if (!_buildingMgr.HasBuilding)
                {
                    Debug.LogWarning("There is no building left to target!");
                    yield break;
                }

                SpawnMissile();
            }
        }

        private void OnMissileDestroyed(RecyclableObject missile)
        {
            ReturnMissileToPool(missile);
            onMissileDestroyed?.Invoke();
        }

        private void OnMissileOutOfScreen(RecyclableObject missile)
        {
            ReturnMissileToPool(missile);
        }

        private void ReturnMissileToPool(RecyclableObject missile)
        {
            missile.onDestroyed -= this.OnMissileDestroyed;
            missile.onOutOfScreen -= this.OnMissileOutOfScreen;
            int index = _missiles.IndexOf(missile);
            _missiles.RemoveAt(index);

            _missileFactory.ReturnToPool(missile);
        }
    }
}