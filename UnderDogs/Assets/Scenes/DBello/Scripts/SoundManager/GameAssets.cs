using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    //Create Game Asset Game Object
    public static GameAssets i
    {
        get
        {

            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    //Create SoundFXClipArray
    public SoundAudioClip[] soundFXAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
        public ParticleSystem particleEffect;
    }

    /*public ParticleEffectClip[] particleFXClipArray;

    [System.Serializable]
    public class ParticleEffectClip
    {
        public SoundManager.Sound sound;
        public ParticleSystem particleEffect;
    }*/
}
