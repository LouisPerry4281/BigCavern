using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPingDot : MonoBehaviour
{
    Color baseColor;
    SpriteRenderer sr;
    float timer;
    float fadeMultiplier;
    float distance;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        baseColor = sr.color;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Color newColor = baseColor;
            newColor.a = 1;
            sr.color = newColor;
            StartCoroutine(FadeOut(sr.color, baseColor, distance * fadeMultiplier));
        }
    }

    IEnumerator FadeOut(Color start, Color end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            sr.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        sr.color = end; //without this, the value will end at something like 0.9992367
    }

    public void InitialiseDot(float distanceFromOrigin, float waveSpeed, float fade)
    {
        distance = distanceFromOrigin;
        timer = distance * waveSpeed;

        fadeMultiplier = fade;
    }
}
