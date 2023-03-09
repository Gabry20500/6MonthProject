using System.Collections;
using UnityEngine;

public static class Utilities
{
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
}
