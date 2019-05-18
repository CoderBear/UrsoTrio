using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
#region Fields
    public AudioClip[] musicClips;
    public AudioClip[] winClips;
    public AudioClip[] loseClips;
    public AudioClip[] bonusClips;

    [Range(0,1)]
    public float musicVolume = 0.5f;
    [Range(0,1)]
    public float fxVolume = 0.5f;

    public float lowPitch = 0.95f;
    public float highPitch = 1.05f;

    public string musicSourceName = "BackgroundMusic";

    // False = new music on reload, true = same music on reload
    public bool dontDestroyMusicOnLoad = true;

#endregion
    // Start is called before the first frame update
    void Start()
    {
        PlayRandomMusic();
    }

#region Methods
    public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1f, bool randomizePitch = true, bool selfdestruct = true)
    {
        if(clip != null)
        {
            GameObject go = new GameObject("SoundFX" + clip.name);
            go.transform.position = position;

            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;

            if(randomizePitch)
            {
                float randomPitch = Random.Range(lowPitch, highPitch);
                source.pitch = randomPitch;
            }

            source.volume = volume;

            source.Play();

            if(selfdestruct)
            {
                Destroy(go, clip.length);
            }
            return source;
        }

        return null;
    }

    public AudioSource PlayRandom(AudioClip[] clips, Vector3 position, float volume = 1f, bool randomizePitch = true, bool selfdestruct = true)
    {
        if(clips != null)
        {
            if(clips.Length != 0)
            {
                int randomIndex = Random.Range(0, clips.Length);

                if(clips[randomIndex] != null)
                {
                    AudioSource source = PlayClipAtPoint(clips[randomIndex], position, volume, randomizePitch, selfdestruct);
                    return source;
                }
            }
        }
        return null;
    }

    public void PlayRandomMusic(bool dontDestroyOnLoad)
    {
        GameObject musicObject = GameObject.Find(musicSourceName);

        if(musicObject == null)
        {
            AudioSource source = PlayRandom(musicClips, Vector3.zero, musicVolume, false, false);
            source.loop = true;
            source.gameObject.name = musicSourceName;

            if(dontDestroyOnLoad && source != null)
            {
                DontDestroyOnLoad(source.gameObject);
            }
        }
    }

    public void PlayRandomMusic()
    {
        PlayRandomMusic(dontDestroyMusicOnLoad);
    }

    public void PlayWinSound()
    {
        PlayRandom(winClips, Vector3.zero, fxVolume);
    }

    public void PlayLoseSound()
    {
        PlayRandom(loseClips, Vector3.zero, fxVolume * 0.5f);
    }

    public void PlayBonusSound()
    {
        PlayRandom(bonusClips, Vector3.zero, fxVolume);
    }
#endregion
}
