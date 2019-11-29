using System;
using UnityEngine;

namespace MissileCommander
{
    public class BulletLauncher : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform firePosition;
        [SerializeField] private float fireDelay = 0.5f;
        private float _elapsedFireTime;
        private bool _canShoot = true;

        private Factory _bulletFactory;

        private void Awake()
        {
            _bulletFactory = new Factory(bulletPrefab);
        }

        private void Update()
        {
            if (!_canShoot)
            {
                _elapsedFireTime += Time.deltaTime;
                if (_elapsedFireTime >= fireDelay)
                {
                    _canShoot = true;
                    _elapsedFireTime = 0f;
                }
            }
        }

        public void OnFireButtonPressed(Vector3 mousePosition)
        {
            if (!_canShoot) { return; }
            
            // Instantiate Bullet
            Bullet bullet = _bulletFactory.Get() as Bullet;
            bullet.Activate(firePosition.position, mousePosition);
            bullet.OnDestroyed += OnBulletDestroyed;

            _canShoot = false;
        }

        public void OnBulletDestroyed(Bullet usedBullet)
        {
            usedBullet.OnDestroyed -= OnBulletDestroyed;
            _bulletFactory.ReturnToPool(usedBullet);
        }
    }
}