using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundLibrary))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundLibrary m_SoundLibrary = null;

    [Range(0, 1)] public float MasterVolume = 1f;
    [Range(0, 1)] public float MusicVolume = 1f;
    [Range(0, 1)] public float SfxVolume = 1f;

    private AudioSource m_MusicSource = null;

    private void Awake()
    {
        m_MusicSource = gameObject.AddComponent<AudioSource>();
        m_MusicSource.spatialBlend = 0;
        m_MusicSource.loop = true;
        m_MusicSource.playOnAwake = false;
    }

    //public void PlaySound(AudioClip clip)
    //{

    //}

    //public void PlaySound(string clipName)
    //{
    //    AudioClip clip = m_SoundLibrary.GetClipFromName(clipName);
    //    PlaySound(clip);
    //}

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        Debug.Log("Playing clip at position: " + position);
        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, MasterVolume * SfxVolume);
        }
    }

    public void PlaySound(string clipName, Vector3 position)
    {
        AudioClip clip = m_SoundLibrary.GetClipFromName(clipName);
        PlaySound(clip, position);
    }

    public void PlayMusic(string name)
    {
        AudioClip clip = m_SoundLibrary.GetClipFromName(name);
        PlayMusic(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if(clip != null)
        {
            m_MusicSource.clip = clip;
            m_MusicSource.volume = MasterVolume * MusicVolume;
            m_MusicSource.Play();
        }
    }
}
