using UnityEngine;

namespace MissileCommander
{
    public class BulletLauncher : MonoBehaviour
    {
        public void OnFireButtonPressed(Vector3 position)
        {
            Debug.Log(position);
        }
    }
}