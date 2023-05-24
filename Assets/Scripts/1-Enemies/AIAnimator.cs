using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public enum ChargeDirection
{
    NE,
    SE,
    SW,
    NW
}


public class AIAnimator : EAnimator
{    
    private NavMeshAgent agent;
    private ChargerAI ai;
    private SpriteRenderer _renderer;

    private Vector2 targetDir;
    private ChargeDirection lastChargeDir;
    private string chargPrefix = "Charging_Phase_";

    private Vector2 dir;
    private Vector2 last;
    public bool charging = false;
    public bool dashing = false;
    private void Awake()
    {
        ai = GetComponentInParent<ChargerAI>();
        lastDir = Direction.S;
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (dashing == false)
        {
            if (charging == true)
            {
                Debug.Log("Cahrging");
                targetDir = new Vector2(ai.target.transform.position.x - 
                                        gameObject.transform.parent.transform.position.x,
                                        ai.target.transform.position.z -
                                        gameObject.transform.parent.transform.position.z);
                SetChargingDirection(targetDir);
                return;
            }
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

    public IEnumerator Charging_Color(float duration)
    {
        float buffer = 0.0f;
        while(buffer < duration)
        {
            _renderer.color = new Color(1, 1 - (buffer / duration), 1 - (buffer / duration));
            buffer += Time.deltaTime;
            yield return null;
        }
        _renderer.color = new Color(1, 1, 1);
    }

    private ChargeDirection newDir;
    private void SetChargingDirection(Vector2 dir)
    {
        newDir = DirectionChargeIndex(dir);
        if(newDir != lastChargeDir)
        {
            lastChargeDir = newDir;
            _animator.Play(chargPrefix + lastChargeDir.ToString());
        }
    }

    protected ChargeDirection DirectionChargeIndex(Vector2 direction)
    {
        Vector2 norDir = direction.normalized;
        Debug.Log(norDir);
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
