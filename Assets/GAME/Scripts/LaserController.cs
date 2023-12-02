using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace GAME.Scripts
{
    public class LaserController : MonoBehaviour
    {
        public GameObject laserOffset;
        private const int MaxLaserLength = 1000;

        private bool isHitMeltingIce = false;
        private MeltingIceController currentMeltingIceController = null;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            CheckRay();
            CheckOrientation();
        }

        private void CheckOrientation()
        {
            var currentAngle = transform.eulerAngles;
            currentAngle.x = 0;
            currentAngle.z = 0;
            transform.eulerAngles = currentAngle;
        }

        private void CheckRay()
        {
            Debug.DrawRay(transform.position, transform.forward, Color.blue, 1f);
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out var hit, MaxLaserLength))
            {
                if (hit.transform.CompareTag("MeltingIce") && !isHitMeltingIce) HitMeltingIce(hit.transform);
                else if (!hit.transform.CompareTag("MeltingIce") && isHitMeltingIce) ReleaseMeltingIce();
                var scale = laserOffset.transform.localScale;
                scale.z = hit.distance / 2;
                laserOffset.transform.localScale = scale;
            }
            else
            {
                if (isHitMeltingIce) ReleaseMeltingIce();
                var scale = laserOffset.transform.localScale;
                scale.z = MaxLaserLength;
                laserOffset.transform.localScale = scale;
            }
        }

        private void HitMeltingIce(Transform transform)
        {
            isHitMeltingIce = true;
            currentMeltingIceController = transform.gameObject.GetComponent<MeltingIceController>();
            currentMeltingIceController.AddHit();
        }

        private void ReleaseMeltingIce()
        {
            if (!currentMeltingIceController.IsDestroyed()) currentMeltingIceController.RemoveHit();
            isHitMeltingIce = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player1") && !other.CompareTag("Player2")) return;
            
            var otherPos = other.transform.position;
            otherPos = otherPos - transform.position;
            var otherX = Vector3.Dot(otherPos, transform.right);
            var otherZ = Vector3.Dot(otherPos, transform.forward);
            var otherPosInLaserCoord = new Vector3(otherX, 0.0f, otherZ);
            if (IsInFrontOfHandle(otherPosInLaserCoord))
            {
                Debug.Log($"in front");
                this.transform.Rotate(Vector3.up, -15.0f);
            }
            else if (IsInBackOfHandle(otherPosInLaserCoord))
            {
                Debug.Log($"in back");
                this.transform.Rotate(Vector3.up, 15.0f);
            }
        }

        private bool IsInFrontOfHandle(Vector3 localOtherPos)
        {
            return localOtherPos.x < -0.5f && localOtherPos.z > 0.0f;
        }

        private static bool IsInBackOfHandle(Vector3 localOtherPos)
        {
            return localOtherPos.x < -0.5f && localOtherPos.z < 0.0f;
        }
    }
}