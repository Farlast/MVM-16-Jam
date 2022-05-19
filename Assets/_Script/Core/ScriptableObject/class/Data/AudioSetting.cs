using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "ScriptableObject/Audio/AudioSetting")]
public class AudioSetting : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float spatialBlend;
    public bool loop;
    public AudioMixerGroup OutputAudioMixerGroup = null;
}
