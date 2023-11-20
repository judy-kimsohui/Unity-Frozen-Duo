using System;
using UnityEngine;

namespace GAME.Scripts
{
    public class ArcticWaterController : MonoBehaviour
    {
        private GameManager gm;

        // Start is called before the first frame update
        private void Start()
        {
            gm = GameManager.Instance;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player1") || other.CompareTag("Player2")) gm.GameOver();
        }
    }
}