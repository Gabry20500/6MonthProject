using System.Collections.Generic;
using UnityEngine;

    public class Boss : Enemy
    {
        protected override void Death()
        {
            List<GameObject> stone = LevelManager.instance.UsableStone;
            if (stone.Count > 0)
            {
                int randStone = Random.Range(0, stone.Count);
                Vector3 enemyPos = transform.position;
                Instantiate(stone[randStone],new Vector3(enemyPos.x, stone[randStone].transform.position.y,enemyPos.z),stone[randStone].transform.rotation);
                
            }

            DoorManager door = FindObjectOfType<DoorManager>();
            door.enemySpawned = false;
            
            NextLevel ladder = FindObjectOfType<NextLevel>();
            ladder.Activate();
            base.Death();
        }

        public void initBar()
        {
            InitHealthBar();
        }
    }