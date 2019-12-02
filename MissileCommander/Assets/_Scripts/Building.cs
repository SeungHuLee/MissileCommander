using System;
using UnityEngine;

namespace MissileCommander
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Building : MonoBehaviour
    {
        public bool isSpawned = false;
        
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.isTrigger = true;
        }
    }
}