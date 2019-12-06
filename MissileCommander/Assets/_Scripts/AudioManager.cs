using System;
using UnityEngine;

namespace MissileCommander
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        [SerializeField] private AudioStorage soundStorage;

        private void Awake()
        {
            if (object.ReferenceEquals(null, Instance))
            {
                Instance = this;
            }
            else { Destroy(this); }
        }

        public void PlaySound(SoundID ID)
        {
            AudioSource.PlayClipAtPoint(soundStorage.Get(ID), Vector3.zero);
        }
    }
}