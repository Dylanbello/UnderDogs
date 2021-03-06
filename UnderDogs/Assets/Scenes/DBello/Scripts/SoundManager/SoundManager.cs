using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Requires Void Awake{ "SoundManager.Initialize(); }" on Dog Characters 
//2D Requires SoundManager.PlaySound(SoundManager.Sound SoundName);
//3D Requires SoundManager.PlaySound(SoundManager.Sound SoundName, GetPosition());
public static class SoundManager
{
    
    public enum Sound
    {
        PlayerIdle,
        PlayerMove,
        PlayerJump,
        PlayerLand,
        PlayerAttack,
        PlayerTakeDamage,
        PlayerDie,
        EnemyIdle,
        EnemyMove,
        EnemyDetection,
        EnemyAttack,
        EnemyTakeDamage,
        EnemyDie,
        CogCollected,
        HeartCollected,
        UISelection
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    //Used for Player Move time variable
    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0;
    }

    //Play 3D Sound
    public static void Play3DSound(Sound sound, Vector3 position, float volume)
    {
        if (canPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.volume = volume;
            //ParticleSystem particleEffect = soundGameObject.AddComponent<ParticleSystem>();
            audioSource.clip = GetAudioClip(sound);
            //particleEffect = GetParticleEffect(sound);
            //audioSource.volume = 0.3f;
            //audioSource.spatialBlend = 1f;
            //audioSource.time = .15f;
            //audioSource.pitch = Random.Range(0.5f, 0.6f);
            //audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.Play();
            //particleEffect.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    //Play 2D Sound
    public static void Play2DSound(Sound sound, float volume)
    {
        if (canPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.volume = volume;

            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    //Dog Move Time
    private static bool canPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .15f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
        }
    }

    //Assign Clips in ClipArray
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundFXAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " Not Found!");
        return null;
    }

    /*private static ParticleSystem GetParticleEffect(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundFXAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.particleEffect;
            }
        }
        Debug.LogError("Particle " + sound + " Not Found!");
        return null;
    }*/
}
