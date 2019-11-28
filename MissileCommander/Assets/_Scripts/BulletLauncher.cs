using UnityEngine;

namespace MissileCommander
{
    public class BulletLauncher : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform firePosition;
        
        public void OnFireButtonPressed(Vector3 mousePosition)
        {
            // Instantiate Bullet
            Bullet bullet = Instantiate<Bullet>(bulletPrefab);
            bullet.Activate(firePosition.position, mousePosition);
        }
    }
}