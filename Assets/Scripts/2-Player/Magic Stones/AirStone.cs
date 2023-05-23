using UnityEngine;

[CreateAssetMenu(fileName = "AirStone", menuName = "Stone/New AirStone", order = 3)]
public class AirStone : Stone
{
    [SerializeField] private float speedBoostPercentage = 15.0f;
    public override void OnSelected(Sword sword) 
    {
        base.OnSelected(sword);
        float maxSpeed = sword.Player_Movement.self.move_Max_Speed;
        sword.Player_Movement.self.move_Max_Speed += (maxSpeed / 100) * speedBoostPercentage;
    }
    public override void OnDeselected(Sword sword) 
    {
        sword.Player_Movement.self.move_Max_Speed = (sword.Player_Movement.self.move_Max_Speed * 100) / (100 + speedBoostPercentage);
    }
}
