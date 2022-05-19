using UnityEngine;

namespace Script.Core
{
    public enum ExitLocationIndex
    {
        StartGameZone,
        StartZone,
        StartZone2,
        Cave,
        Cave2, 
        Cave3
    }
    [CreateAssetMenu(menuName = "ScriptableObject/Scene/SceneContainer")]
    public class SceneData : ScriptableObject
    {
        public string SceneName;
        public int SceneIndex;
        public ExitLocationIndex MoveToEnteranceTag;
    }
}
