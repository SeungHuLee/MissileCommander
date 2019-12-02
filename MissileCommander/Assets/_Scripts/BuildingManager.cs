using UnityEngine;
using System.Collections.Generic;

namespace MissileCommander
{
    public class BuildingManager
    {
        private Building _buildingPrefab;
        private Transform[] _buildingLocators;
        
        private List<Building> _buildings = new List<Building>();

        public BuildingManager(Building buildingPrefab, Transform[] buildingLocators)
        {
            this._buildingPrefab = buildingPrefab;
            this._buildingLocators = buildingLocators;
            
            Debug.Assert(this._buildingPrefab != null, "BuildingManager : Prefab is null!");
            Debug.Assert(this._buildingLocators != null, "BuildingManager : Locators are null!");
            
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
                _buildings.Add(building);
            }
        }
    }
}