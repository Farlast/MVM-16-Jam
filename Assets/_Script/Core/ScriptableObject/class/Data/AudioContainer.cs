using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Audio/AudioContainer")]
public class AudioContainer : ScriptableObject
{
    [SerializeField] private Sound[] sounds;

    public Sound[] GetSounds() => sounds;
}

[System.Serializable]
public class Sound
{
    public AudioClip audioClip;
    public string name;

    [HideInInspector]
    public AudioSource audioSource;
}
