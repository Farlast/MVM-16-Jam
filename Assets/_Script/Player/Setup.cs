using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class Setup : MonoBehaviour
    {
        [SerializeField] PlayerStatus status;
        [SerializeField] Transform playerTransform;
        [SerializeField] bool setStartpoint;
        [SerializeField] SaveData saveData;
        void Start()
        {
            status.HasDash = false;
            status.HasDoubleJump = false;
            status.HasWallJump = false;
            status.SavePosition = playerTransform.position;
            saveData.ResetGameState();

            if (setStartpoint)
                status.SavePosition = Vector3.zero;
        }

    }
}
