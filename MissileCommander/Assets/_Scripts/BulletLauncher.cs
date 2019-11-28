using System;
using UnityEngine;

namespace MissileCommander
{
    public class BulletLauncher : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform firePosition;

        private Factory _bulletFactory;

        private void Awake()
        {
            _bulletFactory = new Factory(bulletPrefab);
        }

        public void OnFireButtonPressed(Vector3 mousePosition)
        {
            // Instantiate Bullet
            Bullet bullet = _bulletFactory.Get() as Bullet;
            bullet.Activate(firePosition.position, mousePosition);
        }
    }
}