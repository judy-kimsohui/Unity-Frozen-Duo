using System;
using UnityEngine;

namespace GAME.Scripts
{
    public class ArcticGoalAreaController : MonoBehaviour
    {

        private GameManager gm;
        private bool isPlayer1GoalIn = false;
        private bool isPlayer2GoalIn = false;
        
        // Start is called before the first frame update
        void Start()
        {
            gm = GameManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player1")) isPlayer1GoalIn = true;
            else if (other.CompareTag("Player2")) isPlayer2GoalIn = true;
            else return;
            
            if(isPlayer1GoalIn && isPlayer2GoalIn) gm.GameClear();
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player1")) isPlayer1GoalIn = false;
            else if (other.CompareTag("Player2")) isPlayer2GoalIn = false;
        }
    }
}
