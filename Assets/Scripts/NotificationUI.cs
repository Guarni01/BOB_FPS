using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    public static NotificationUI Instance;

    [SerializeField] private TextMeshProUGUI notificationText;

    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float displayDuration = 2f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        Instance = this;

        Color c = notificationText.color;
        c.a = 0;
        notificationText.color = c;
        notificationText.text = "";
    }

    public void ShowMessage(string message)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(message));
    }

    IEnumerator ShowRoutine(string message)
    {
        notificationText.text = message;

        // Fade In
        yield return Fade(0, 1);

        // Rimane visibile
        yield return new WaitForSeconds(displayDuration);

        // Fade Out
        yield return Fade(1, 0);

        notificationText.text = "";
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float t = 0;

        Color c = notificationText.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);
            notificationText.color = c;

            yield return null;
        }

        c.a = endAlpha;
        notificationText.color = c;
    }
}