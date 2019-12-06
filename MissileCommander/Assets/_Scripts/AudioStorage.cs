using System;
using UnityEngine;
using Devcat;

namespace MissileCommander
{
    [CreateAssetMenu]
    public class AudioStorage : ScriptableObject
    {
        [SerializeField] private SoundSrc[] soundSrcs;
        EnumDictionary<SoundID, AudioClip> _soundDictionary = new EnumDictionary<SoundID, AudioClip>();
        
        private void GenerateDictionary()
        {
            for (int i = 0; i < soundSrcs.Length; i++)
            {
                _soundDictionary.Add(soundSrcs[i].SoundID, soundSrcs[i].SoundFile);
            }
        }

        public AudioClip Get(SoundID ID)
        {
            Debug.Assert(soundSrcs.Length > 0, "AudioStorage : SoundSrcs is empty!");

            if (_soundDictionary.Count == 0)
            {
                GenerateDictionary();
            }

            return _soundDictionary[ID];
        }
    }

    public enum SoundID
    {
        Shoot, BulletExplosion, BuildingExplosion, GameOver
    }

    [Serializable]
    public struct SoundSrc
    {
        [SerializeField] private AudioClip soundFile;
        [SerializeField] private SoundID soundID;

        public AudioClip SoundFile => soundFile;
        public SoundID SoundID => soundID;
        
        
    }
}