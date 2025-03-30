using UnityEngine;
using static Tec.TecVector;

namespace Tec
{
    namespace CameraFollow
    {
        public static class CameraFollowSystem
        {
            // Method that logs "Hello World"
            public static void CameraFollow(Transform target)
            {
                Vector3 PlayerPlace = target.position;
                PlayerPlace += GameDataBase.Instance.GetCameraOffset();
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, PlayerPlace, GameDataBase.Instance.GetCameraSmoothness() * Time.deltaTime);
            }
        }
    }
    namespace RayCheck
    {
        public static class RayCheckSystem
        {
            static RaycastHit2D hitRight;
            static RaycastHit2D hitLeft;
            public static bool RayCheck(Transform target, LayerMask layerMask, float rayOffset, float rayDistance, DuoVector duoVector)
            {
                Vector2 leftRayOrigin = (Vector2)target.position + duoVector.Vector1 * rayOffset;
                Vector2 rightRayOrigin = (Vector2)target.position + duoVector.Vector2 * rayOffset;

                hitLeft = Physics2D.Raycast(leftRayOrigin, duoVector.Vector1, rayDistance, layerMask);
                Debug.DrawRay(leftRayOrigin, duoVector.Vector1 * rayDistance, Color.red);

                hitRight = Physics2D.Raycast(rightRayOrigin, duoVector.Vector2, rayDistance, layerMask);
                Debug.DrawRay(rightRayOrigin, duoVector.Vector2 * rayDistance, Color.blue);

                if (hitRight || hitLeft)
                {
                    return true;
                }
                return false;
            }
            public static RaycastHit2D GetRayCastRight()
            {
                return hitRight;
            }
            public static RaycastHit2D GetRayCastLeft()
            {
                return hitLeft;
            }

        }
    }
    namespace DistanceCheck
    {
        public static class DistanceCheckSystem
        {
            static float VectorDistance;
            public static bool DistanceObject(GameObject Target, GameObject Self, float MaxDistance)
            {
                VectorDistance = Vector3.Distance(Self.transform.position, Target.transform.position);
                if (VectorDistance <= MaxDistance)
                {
                    return true;
                }
                return false;

            }
            public static float GetDistance()
            {
                Debug.Log(VectorDistance);
                return VectorDistance;
            }
        }
    }
    namespace MovmentSysteam
    {
        public static class MovmentSysteam
        {
            
            public static void MoveTo(GameObject Object_, float speed,DuoVector3 duoVector3,bool FlipSide)
            {
                bool Direction = FlipSide ? Object_.GetComponent<SpriteRenderer>().flipX : Object_.GetComponent<SpriteRenderer>().flipY;
                Vector3 vectorDirection = Direction ? duoVector3.Vector1 : duoVector3.Vector2;
                Object_.transform.Translate(vectorDirection * speed * Time.deltaTime, Space.World);
            }
        }
    }

    public class TecVector
    {
        public TecVector()
        {

        }
        public struct DuoGameObject
        {
            public bool Found;
            public GameObject Target;

            public DuoGameObject(GameObject target, bool found)
            {
                Found = found;
                Target = target;
            }
            //return new DuoGameObject(null, false);
        }
        public struct DuoVector
        {
            public Vector2 Vector1;
            public Vector2 Vector2;

            public DuoVector(Vector2 vector1, Vector2 vector2)
            {
                Vector1 = vector1;
                Vector2 = vector2;
            }
            //return new DuoGameObject(null, false);
        }
        public struct DuoVector3
        {
            public Vector3 Vector1;
            public Vector3 Vector2;

            public DuoVector3(Vector3 vector1, Vector3 vector2)
            {
                Vector1 = vector1;
                Vector2 = vector2;
            }
            //return new DuoGameObject(null, false);
        }
    }

}



