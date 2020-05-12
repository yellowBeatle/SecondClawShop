using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    [SerializeField] private List<SoundGroup> m_SoundGroups;

    public AudioClip GetClipFromName(string name)
    {
        foreach(SoundGroup group in m_SoundGroups)
        {
            if(group.name == name)
            {
                return group.clip;
            }
        }

        Debug.LogWarning("The clip with name " + name + " doesn't exist. Please make shure that the name is correct.");
        return null;
    }

    [System.Serializable]
    public class SoundGroup
    {
        public string name = "";
        public AudioClip clip = null;
    }
}
