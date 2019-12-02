/* ===================================================================================
 *  GameManager will work as composition root.
 *  Assigned Jobs :
 *     1. Create instance of a BulletLauncher from the prefab.
 *     2. Create instance of any GameController Class and assign it to BulletLauncher.
 * ===================================================================================*/

using UnityEngine;

namespace MissileCommander
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BulletLauncher launcherPrefab;
        [SerializeField] private Transform launcherLocator;
        [SerializeField] private Building buildingPrefab;
        [SerializeField] private Transform[] buildingLocators;
        
        private BulletLauncher _launcher;
        private BuildingManager _buildingMgr;

        private void Awake()
        {
            _launcher = Instantiate(launcherPrefab);
            _launcher.transform.position = launcherLocator.position;
            
            _buildingMgr = new BuildingManager(buildingPrefab, buildingLocators);

            MouseGameController mouseController = gameObject.AddComponent<MouseGameController>();
            mouseController.OnFireButtonPressed += _launcher.OnFireButtonPressed;
        }
    }
}