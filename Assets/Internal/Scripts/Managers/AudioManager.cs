using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioEnum
{
    BGM = 0,

    Explosion = 1,

    PlayerDamaged = 2,
    EnemyDamaged = 3,
    ThingPlaced = 4,
    GardenDamaged = 5,

    GameOver = 6,
    Ding = 7,
    LevelUp = 8,
    Error = 9,
    BirdCry = 10,
    UhOh = 11,
    EnemyDamagedQuiet = 13,
    ShotgunShoot = 14,
    ExecuteSound = 16,
    CritSound = 17,

    None = 12,
    EmptyBG = 15,
}

// Brackey's Audio Manager
[System.Serializable]
public class Sound
{
    public AudioEnum name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;

    [Range(0f, 1f)]
    public float pitch = 1f;

    [Header("Variance")]
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;

    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    private List<AudioSource> sources = new();
    public int SourceCount = 1;
    [Space(10f)]
    public bool loops = false;
    public bool isMusic = false;
    public bool playOnAwake = false;
    public bool muteOnAwake = false;
    public void SetSource(AudioSource _source)
    {
        sources.Add(_source);
        sources[sources.Count - 1].clip = clip;
    }

    public void ChangeVolume(float vol)
    {
        foreach (AudioSource source in sources)
        {
            if (source != null)
                source.volume = vol * volume;
        }
    }

    public void Play()
    {
        for (int i = 0; i < sources.Count; i++)
        {
            if (sources[i].isPlaying && i != sources.Count-1)
                continue;

            if (isMusic)
            {
                sources[i].volume = GlobalAudio.MusicVolume * volume;
            }
            else
            {
                sources[i].volume = GlobalAudio.SoundVolume * volume * (1 + UnityEngine.Random.Range(-randomVolume / 2f, randomVolume / 2f));
            }
            sources[i].pitch = pitch * (1 + UnityEngine.Random.Range(-randomPitch / 2f, randomPitch / 2f));
            sources[i].loop = loops;

            sources[i].Play();
            return;
        }

        Debug.LogWarning(name.ToString() + " ran out of audio sources");
    }

    public void Stop()
    {
        foreach (AudioSource source in sources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
        
    }
}

public class AudioManager : MonoBehaviour
{
    public bool DebugMuteMusic = false;
    private bool _debugMuteMusic;

    public static AudioManager instance;
    public static Sound CurrentMusic;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Audio Manager found");
        }    
        else
        {
            instance = this;
        }

        _debugMuteMusic = false;
    #if UNITY_EDITOR
        _debugMuteMusic = DebugMuteMusic;
    #endif
    }

    [SerializeField]
    List<Sound> sounds = new List<Sound>();

    public void AdjustMusicVolume(float vol)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].isMusic)
            {
                sounds[i].ChangeVolume(vol);
                return;
            }
        }
    }

    private void Start()
    {
        int index = 0;
        foreach (Sound sound in sounds)
        {
            for(int i = 0; i < sound.SourceCount; i++)
            {
                GameObject _go = new GameObject("Sound_" + index + "_" + sound.name);
                _go.transform.SetParent(transform, false);
                sound.SetSource(_go.AddComponent<AudioSource>());
                if (sound.isMusic)
                {
                    CurrentMusic = sound;
                }

                if (sound.playOnAwake)
                {
                    sound.Play();
                    if (sound.muteOnAwake)
                    {
                        sound.ChangeVolume(0);
                    }
                }
                index++;
            }
            
        }

        if (!_debugMuteMusic)
        {
            StartCoroutine(WaitMusic());
        }
        
    }

    private IEnumerator WaitMusic()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        PlaySound(AudioEnum.BGM);
    }

    public void Mute(AudioEnum _name)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].ChangeVolume(0);
                return;
            }
        }
        Debug.LogWarning(_name + " not found in Audio Manager");
    }

    public void Unmute(AudioEnum _name)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].ChangeVolume(1);
                return;
            }
        }
        Debug.LogWarning(_name + " not found in Audio Manager");
    }

    public void PlaySound(AudioEnum _name)
    {
        if (_name == AudioEnum.None) { return; }

        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning(_name + " not found in Audio Manager");
    }

    public void StopSound(AudioEnum _name)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        Debug.LogWarning(_name + " not found in Audio Manager");
    }
}
