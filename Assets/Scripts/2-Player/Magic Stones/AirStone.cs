using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AirStone", menuName = "Stone/New AirStone", order = 3)]
public class AirStone : Stone
{
    [SerializeField] private float speedBoostPercentage = 15.0f;
    [SerializeField] private float speedBoostDuration = 3.0f;
    public override void OnSelected(Sword sword) 
    {
        base.OnSelected(sword);
        //float maxSpeed = sword.Player_Movement.self.move_Max_Speed;
        //sword.Player_Movement.self.move_Max_Speed += (maxSpeed / 100) * speedBoostPercentage;
    }

    public override void OnDeselected(Sword sword)
    {
        base.OnDeselected(sword);
        //sword.Player_Movement.self.move_Max_Speed = (sword.Player_Movement.self.move_Max_Speed * 100) / (100 + speedBoostPercentage);
    }

    public override void OnEnemyHitted(Sword sword, EnemyAI enemy)
    {
        sword.Player_Movement.StartCoroutine(SpeedUp_Routine(sword));
        sword.UseMana();
    }

    private IEnumerator SpeedUp_Routine(Sword sword)
    {
        float buffer = 0.0f;

        float maxSpeed = sword.Player_Movement.self.move_Max_Speed;
        sword.Player_Movement.self.move_Max_Speed += (maxSpeed / 100) * speedBoostPercentage;

        while (buffer < speedBoostDuration)
        {
            buffer += Time.deltaTime;
            yield return null;
        }

        sword.Player_Movement.self.move_Max_Speed = (sword.Player_Movement.self.move_Max_Speed * 100) / (100 + speedBoostPercentage);
    }
}
