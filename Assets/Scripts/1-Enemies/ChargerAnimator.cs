using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChargeDirection
{ 
    SE,
    NE,
    NW,
    SW
}
public class ChargerAnimator : AIAnimator
{
    protected ChargerAI ai;
    [SerializeField] protected SpriteRenderer _renderer;

    private Vector2 targetDir;
    private ChargeDirection lastChargeDir;
    private ChargeDirection newDir;
    private string chargPrefix = "Charging_Phase_";

    public bool charging = false;
    public bool dashing = false;

    private new void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        base.Awake();
        ai = GetComponentInParent<ChargerAI>();
    }

    private void FixedUpdate()
    {
        if (dashing == false)
        {
            if (charging == true)
            {
                targetDir = new Vector2(ai.target.transform.position.x -
                                        gameObject.transform.parent.transform.position.x,
                                        ai.target.transform.position.z -
                                        gameObject.transform.parent.transform.position.z);
                SetChargingDirection(targetDir);
            }
            else
            {
                dir = new Vector2(agent.velocity.x, agent.velocity.z);
                if (dir.magnitude <= 0.1f)
                {
                    SetDirection(last, true);
                }
                else
                {
                    last = dir;
                    SetDirection(dir);
                }
            }
        }
    }

    public IEnumerator Charging_Color(float duration)
    {
        float buffer = 0.0f;
        while (buffer < duration)
        {
            _renderer.material.SetColor("_Color", new Color(1, 1 - buffer/duration, 1-buffer/duration, 1));
            buffer += Time.deltaTime;
            yield return null;
        }
        _renderer.material.SetColor("_Color", new Color(1, 1,1, 1));
    }

    private bool flag = false;
    private void SetChargingDirection(Vector2 dir)
    {
        newDir = DirectionChargeIndex(dir);
        if (newDir != lastChargeDir)
        {
            lastChargeDir = newDir;
            _animator.Play(chargPrefix + lastChargeDir.ToString());
        }
    }

    protected ChargeDirection DirectionChargeIndex(Vector2 direction)
    {
        Vector2 norDir = -direction.normalized;
        float step = 360 / 4;  //Decide the 8 step of an isometic movement
        //float offset = step / 2; //An offset to be sure never to go under 0
        float angle = Vector2.SignedAngle(Vector2.up, norDir); //Using Vector2.up as reference calculate the angle between him and entity movement dir

        //angle += offset;
        if (angle < 0) //Add 360 to the negative angles to mantain a range 0-360
        {
            angle += 360;
        }
        float stepCount = angle / step; // Calculate in which step we are
        return (ChargeDirection)Mathf.FloorToInt(stepCount);
    }

    private void SetDashAnimation()
    {

    }
}
