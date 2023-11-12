using Unity.VisualScripting;
using UnityEngine;

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
                if(hit.transform.CompareTag("MeltingIce") && !isHitMeltingIce) HitMeltingIce(hit.transform);
                else if(!hit.transform.CompareTag("MeltingIce") && isHitMeltingIce) ReleaseMeltingIce();
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
            if(!currentMeltingIceController.IsDestroyed()) currentMeltingIceController.RemoveHit();
            isHitMeltingIce = false;
        }
    }
}
