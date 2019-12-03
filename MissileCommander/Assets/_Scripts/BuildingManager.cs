using UnityEngine;
using System.Collections.Generic;

namespace MissileCommander
{
    public class BuildingManager
    {
        private Building _buildingPrefab;
        private Transform[] _buildingLocators;

        private Factory _effectFactory;
        
        private List<Building> _buildings = new List<Building>();

        public BuildingManager(Building buildingPrefab, Transform[] buildingLocators, Factory effectFactory)
        {
            this._buildingPrefab = buildingPrefab;
            this._buildingLocators = buildingLocators;
            this._effectFactory = effectFactory;

            Debug.Assert(this._buildingPrefab != null, "BuildingManager : Prefab is null!");
            Debug.Assert(this._buildingLocators != null, "BuildingManager : Locators are null!");
            Debug.Assert(this._effectFactory != null, "BuildingManager : EffectFactory is null!");
        }

        public void OnGameStart()
        {
            SpawnBuildings();
        }
        
        public void SpawnBuildings()
        {
            if (_buildings.Count > 0)
            {
                Debug.Log("BuildingManager : Buildings are already spawned!");
                return;
            }

            for (int i = 0; i < _buildingLocators.Length; i++)
            {
                Building building = Object.Instantiate(_buildingPrefab);
                building.transform.position = _buildingLocators[i].position;
                building.onDestroyed += OnBuildingDestroyed;
                _buildings.Add(building);
            }
        }

        public Vector3 GetRandomBuildingPosition()
        {
            Debug.Assert(_buildings.Count > 0, "BuildingManager : No Building left in the list!");
            Building building = _buildings[Random.Range(0, _buildings.Count)];
            return building.transform.position;
        }

        private void OnBuildingDestroyed(Building building)
        {
            Vector3 lastPos = building.transform.position;
            
            building.onDestroyed -= this.OnBuildingDestroyed;
            int index = _buildings.IndexOf(building);
            _buildings.RemoveAt(index);
            Object.Destroy(building.gameObject);

            RecyclableObject effect = _effectFactory.Get();
            effect.Activate(lastPos);
        }
    }
}