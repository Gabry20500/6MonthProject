using System.Collections;
using UnityEngine;

public static class Utilities
{
    private static Vector3 dir;

    //Freeze frame time scale for given duration of the given slowing value
    public static IEnumerator FreezeFrames(float freezeValue, float freezeDuration)
    {
        Time.timeScale = freezeValue;
        float buffer = 0.0f;
        while(buffer < freezeDuration)
        {
            buffer += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
    }

    //Generic func that find normalized direction between two vector
    public static Vector3 CalculateDir(Vector3 start, Vector3 end)
    {
        dir = start - end;
        dir = new Vector3(dir.x, 0.0f, dir.z);
        return dir.normalized;
    }
}
