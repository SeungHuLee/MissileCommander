using UnityEngine;

namespace MissileCommander
{
    public class KeyGameController : IGameController
    {
        private KeyCode _fireButton = KeyCode.Space;
        
        public bool FireButtonPressed()
        {
            return Input.GetKeyDown(_fireButton);
        }
    }
}