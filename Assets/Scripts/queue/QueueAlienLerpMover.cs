using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class QueueAlienLerpMover : MonoBehaviour
{
    public float moveDuration = 0.35f;

    public Action OnMoveFinished;

    private RectTransform rt;
    private Image img;
    private Coroutine moveRoutine;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        img = GetComponent<Image>(); // für Fade (UI Image)
    }

    // normales Rutschen
    public void MoveTo(Vector2 target)
    {
        StartNewRoutine(MoveRoutine(target, fadeOut: false, destroyOnEnd: false));
    }

    // rausfahren + ausfaden + zerstören
    public void ExitTo(Vector2 target)
    {
        StartNewRoutine(MoveRoutine(target, fadeOut: true, destroyOnEnd: true));
    }

    private void StartNewRoutine(IEnumerator routine)
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(routine);
    }

    private IEnumerator MoveRoutine(Vector2 target, bool fadeOut, bool destroyOnEnd)
    {
        Vector2 start = rt.anchoredPosition;
        float t = 0f;

        // falls kein Image vorhanden, trotzdem bewegen
        Color startColor = (img != null) ? img.color : Color.white;

        float dur = Mathf.Max(0.0001f, moveDuration);

        while (t < 1f)
        {
            t += Time.deltaTime / dur;

            // optional smoother:
            float eased = Mathf.SmoothStep(0f, 1f, t);

            rt.anchoredPosition = Vector2.Lerp(start, target, eased);

            if (fadeOut && img != null)
            {
                float a = Mathf.Lerp(startColor.a, 0f, eased);
                img.color = new Color(startColor.r, startColor.g, startColor.b, a);
            }

            yield return null;
        }

        rt.anchoredPosition = target;

        OnMoveFinished?.Invoke();

        if (destroyOnEnd)
            Destroy(gameObject);
    }
}