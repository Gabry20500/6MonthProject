using UnityEngine;

    public class Boss : Enemy
    {
        protected override void Death()
        {
            NextLevel ladder = FindObjectOfType<NextLevel>();
            ladder.Activate();
            base.Death();
        }
    }