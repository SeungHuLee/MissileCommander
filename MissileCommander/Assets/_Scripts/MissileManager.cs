using UnityEngine;

namespace MissileCommander
{
    public class MissileManager : MonoBehaviour
    {
        [SerializeField] private Missile missilePrefab;
        private Factory _missileFactory;
        private BuildingManager _buildingMgr;

        private bool _isInitialized = false;

        public void Initialize(Factory missileFactory, BuildingManager buildingMgr)
        {
            if (_isInitialized) { return; }
            
            this._missileFactory = missileFactory;
            this._buildingMgr = buildingMgr;
            
            Debug.Assert(_missileFactory != null, "MissileManager : Missile Factory is null!");
            Debug.Assert(_buildingMgr != null, "MissileManager : BuildingManager is null!");

            _isInitialized = true;
        }
    }
}