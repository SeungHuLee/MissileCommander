using System;
using UnityEngine;

namespace MissileCommander
{
    public class MouseGameController : MonoBehaviour, IGameController
    {
        private Camera _mainCamera;
        public event Action<Vector3> FireButtonPressed = delegate {  };

        private void Awake()
        {
            _mainCamera = Camera.main != null ? Camera.main : FindObjectOfType<Camera>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireButtonPressed(GetClickPosition(Input.mousePosition));
            }
        }

        private Vector3 GetClickPosition(Vector3 mousePosition)
        {
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(mousePosition);
            worldPos.z = 0f;
            return worldPos;
        }
    }
}