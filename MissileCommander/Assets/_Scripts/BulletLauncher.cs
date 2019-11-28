using System;
using UnityEngine;

namespace MissileCommander
{
    public class BulletLauncher : MonoBehaviour
    {
        private IGameController _controller;


        public void SetGameController(IGameController controller)
        {
            _controller = controller;
        }
        
        private void Update()
        {
            if (_controller != null)
            {
                if (_controller.FireButtonPressed())
                {
                    Debug.Log("Bullet Fired!");
                }
            }
            else
            {
                Debug.Log("GameController is null!");
            }
        }

    }
}