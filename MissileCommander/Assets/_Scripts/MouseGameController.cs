using UnityEngine;

namespace MissileCommander
{
    public class MouseGameController : IGameController
    {
        public bool FireButtonPressed()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}