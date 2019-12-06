using System;
using UnityEngine;

namespace MissileCommander
{
    public class BulletLauncher : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Explosion explosionPrefab;
        [SerializeField] private Transform firePosition;
        [SerializeField] private float fireDelay = 0.5f;
        private float _elapsedFireTime;
        private bool _canShoot = true;
        private bool _isGameStarted = false;

        private Factory _bulletFactory;
        private Factory _explosionFactory;

        private void Awake()
        {
            _bulletFactory = new Factory(bulletPrefab);
            _explosionFactory = new Factory(explosionPrefab);
        }

        private void Update()
        {
            if (!_isGameStarted) { return; }
            
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

        public void OnGameStart()
        {
            _isGameStarted = true;
        }

        public void OnFireButtonPressed(Vector3 mousePosition)
        {
            if (!_isGameStarted) { return; }
            if (!_canShoot) { return; }
            
            // Instantiate Bullet
            RecyclableObject bullet = _bulletFactory.Get();
            bullet.Activate(firePosition.position, mousePosition);
            bullet.onDestroyed += OnBulletExplode;
            
            AudioManager.Instance.PlaySound(SoundID.Shoot);

            _canShoot = false;
        }

        public void OnBulletExplode(RecyclableObject usedBullet)
        {
            Vector3 lastBulletPos = usedBullet.CachedTransform.position;
            usedBullet.onDestroyed -= OnBulletExplode;
            _bulletFactory.ReturnToPool(usedBullet);
            
            RecyclableObject explosion = _explosionFactory.Get();
            explosion.Activate((lastBulletPos));
            explosion.onDestroyed += OnExplosionDestroyed;
            
            AudioManager.Instance.PlaySound(SoundID.BulletExplosion);
        }

        private void OnExplosionDestroyed(RecyclableObject explosion)
        {
            explosion.onDestroyed -= OnExplosionDestroyed;
            _explosionFactory.ReturnToPool(explosion);
        }

        public void OnGameOver(bool isVictory, int buildingCount)
        {
            _isGameStarted = false;
        }
    }
}