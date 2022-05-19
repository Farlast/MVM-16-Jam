using UnityEngine;

namespace Script.Core
{
    public class FieldOfView : MonoBehaviour
    {
        [Header("FieldOfView settings")]
        [SerializeField] private float Radius;
        [Range(0,360)]
        [SerializeField] private float Angle;
        [SerializeField] private float CloseAwareness;
        [SerializeField] public LayerMask TargrtLayer;
        [SerializeField] public LayerMask Ground;
        [field: SerializeField] public bool SeeTarget { get; private set; }
        [field: SerializeField] public Vector3 TargetPosition { get; private set;}
        [field: SerializeField] public Vector3 DirectionToPlayer { get; private set; }
        [field: SerializeField] public float DistanceToTarget { get; private set; }

        private void FixedUpdate()
        {
            FieldOfViewCheck();
        }
        private void FieldOfViewCheck()
        {
            var target = Physics2D.OverlapCircle(transform.position, Radius, TargrtLayer);
            
            if (target == null) 
            { 
                SeeTarget = false;
                return; 
            }

            if (!target.TryGetComponent(out IDamageable damageable))
            {
                SeeTarget = false;
                return;
            }

            TargetPosition = target.transform.position;
            DirectionToPlayer = Helpers.GetDirection(transform.position, TargetPosition);
            DistanceToTarget = Vector3.Distance(transform.position, TargetPosition);
           
            if (CheckeInAngle(TargetPosition, DirectionToPlayer) && !IsBlockByObject(TargetPosition, DirectionToPlayer))
            {
                SeeTarget = true;
            }
            else if(DirectionToPlayer.x < CloseAwareness && DirectionToPlayer.y < CloseAwareness)
            {
                SeeTarget = true;
            }
            else
            {
                SeeTarget = false;
            }

        }
        private bool IsBlockByObject(Vector3 targetPosition, Vector3 Direction)
        {
            // check is some object block view.
            return Physics2D.Raycast(transform.position, Direction, DistanceToTarget, Ground);
        }
        private bool CheckeInAngle(Vector3 targetPosition, Vector3 Direction)
        {
            return Vector2.Angle(transform.forward, Direction) < Angle / 2 ;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var Direction = Helpers.GetDirection(transform.position, TargetPosition);
            Gizmos.DrawLine(transform.position, transform.position + Direction * DistanceToTarget);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Radius);

            Vector3 ang1 = Helpers.DirectionFromAngle2D(transform.eulerAngles.y, -Angle / 2);
            Vector3 ang2 = Helpers.DirectionFromAngle2D(transform.eulerAngles.y, Angle / 2);

            Gizmos.DrawLine(transform.position, transform.position + ang1 * Radius);
            Gizmos.DrawLine(transform.position, transform.position + ang2 * Radius);
        }   
    }
}
