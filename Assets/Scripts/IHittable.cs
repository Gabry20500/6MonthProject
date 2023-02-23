using UnityEngine;
using System.Collections;
public interface IHittable 
{
    public abstract void OnHit(Collision collision);
    public abstract IEnumerator KnockbackStunTime(float cooldown);

}
