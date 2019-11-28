using System;
using UnityEngine;

namespace MissileCommander
{
    [Obsolete]
    public class KeyGameController : MonoBehaviour, IGameController
    {
        [SerializeField] private KeyCode fireButton = KeyCode.Space;
        public event Action FireButtonPressed = delegate {  };

        private void Update()
        {
            if (Input.GetKeyDown(fireButton))
            {
                FireButtonPressed();
            }
        }
    }
}