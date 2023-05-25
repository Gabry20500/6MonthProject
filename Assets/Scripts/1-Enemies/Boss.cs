using UnityEngine;

    public class Boss : Enemy
    {
        protected override void Death()
        {
            DoorManager door = FindObjectOfType<DoorManager>();
            door.enemySpawned = false;
            
            NextLevel ladder = FindObjectOfType<NextLevel>();
            ladder.Activate();
            base.Death();
        }
    }