using UnityEngine;
using UnityEngine.Events;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/Event/Scene event channel")]
    public class SceneEventChannel : ScriptableObject
    {
        public UnityAction<SceneData> onEventRaised;

        public void RiseEvent(SceneData sceneLinkContainer)
        {
            onEventRaised?.Invoke(sceneLinkContainer);
        }
    }
    /*
   player need to enter A and exit to B position

   enter side [ location entrance ]
   - send this scene data to system
   - send entrance data to system
   - stop player fade screen

   system side [ scene loader ]
   - get data from enterance
   - set where scene to load
   * load new scene
   - find where to set player position by enterance data with enterance Map
   - store to new map
   - get map destination and set player when exit side ask

   exit side [ location entrance ]
   * on start ask system where to put player
   - where scene come from
   - where entrance
   * set player location

    */
}
