using UnityEngine;

namespace GAME.Scripts
{
    public class LaserController : MonoBehaviour
    {
        public GameObject laserOffset;

        private const int MaxLaserLength = 1000;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out var hit, MaxLaserLength))
            {
                var scale = laserOffset.transform.localScale;
                scale.z = hit.distance / 2;
                laserOffset.transform.localScale = scale;
            }
            else
            {
                var scale = laserOffset.transform.localScale;
                scale.z = MaxLaserLength;
                laserOffset.transform.localScale = scale; 
            }
        }
    }
}
