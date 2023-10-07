namespace VUDK.Features.Main.Camera
{
    using System;
    using System.Collections;
    using UnityEngine;

    public class Fade : MonoBehaviour
    {
        public enum StartFade
        {
            None,
            FadeIn,
            FadeOut,
            FadeInFadeOut,
            FadeOutFadeIn
        }

        [Tooltip("Fade duration time in seconds"), Range(1, 100)]
        public float FadeDuration = 1f;
        public Color FadeColor;

        [Tooltip("Fade on Start")]
        public StartFade FadeStart = StartFade.None;

        private float _alpha = 0f;

        public event Action OnFadeInStart;
        public event Action OnFadeInEnd;
        public event Action OnFadeOutStart;
        public event Action OnFadeOutEnd;

        private void Start()
        {
            if (FadeStart == StartFade.None)
                return;

            switch (FadeStart)
            {
                case StartFade.FadeIn:
                    DoFadeIn(FadeDuration);
                    break;

                case StartFade.FadeOut:
                    DoFadeOut(FadeDuration);
                    break;

                case StartFade.FadeInFadeOut:
                    DoFadeInOut(FadeDuration);
                    break;

                case StartFade.FadeOutFadeIn:
                    DoFadeOutIn(FadeDuration);
                    break;
            }
        }

        /// <summary>
        /// Starts fade out effect.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        public void DoFadeOut(float time)
        {
            StartCoroutine(FadeOutRoutine(time));
        }

        /// <summary>
        /// Starts fade out effect.
        /// </summary>
        public void DoFadeOut()
        {
            DoFadeOut(FadeDuration);
        }

        /// <summary>
        /// Starts fade in effect.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        public void DoFadeIn(float time)
        {
            StartCoroutine(FadeInRoutine(time));
        }

        /// <summary>
        /// Starts fade in effect.
        /// </summary>
        public void DoFadeIn()
        {
            DoFadeIn(FadeDuration);
        }

        /// <summary>
        /// Starts fade out followed by fade in.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        public void DoFadeOutIn(float time)
        {
            StartCoroutine(FadeOutInRoutine(time));
        }

        /// <summary>
        /// Starts fade out followed by fade in.
        /// </summary>
        public void DoFadeOutInt()
        {
            DoFadeInOut(FadeDuration);
        }

        /// <summary>
        /// Starts fade in followed by fade out.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        public void DoFadeInOut(float time)
        {
            StartCoroutine(FadeInOutRoutine(time));
        }

        /// <summary>
        /// Starts fade in followed by fade out.
        /// </summary>
        public void DoFadeInOut()
        {
            DoFadeInOut(FadeDuration);
        }

        /// <summary>
        /// Coroutine fading in followed by fading out.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        private IEnumerator FadeInOutRoutine(float time)
        {
            yield return FadeInRoutine(time);
            yield return FadeOutRoutine(time);
        }

        /// <summary>
        /// Coroutine fading out followed by fading in.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        private IEnumerator FadeOutInRoutine(float time)
        {
            yield return FadeOutRoutine(time);
            yield return FadeInRoutine(time);
        }

        /// <summary>
        /// Coroutine fading out.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        private IEnumerator FadeOutRoutine(float time)
        {
            OnFadeOutStart?.Invoke();

            float startAlpha = 1f; // Alpha is 1 so the FadeOut start with a DrawTexture already fully visible
            float progress = 0f;

            // Continuously update the alpha value while the progress is less than 1.
            while (progress < 1f)
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime / time); // clamping the progress value to the range of 0 to 1, the fading effect is always controlled and never exceeds the desired fadeDuration
                _alpha = Mathf.Lerp(startAlpha, 0f, progress);
                yield return null;
            }

            OnFadeOutEnd?.Invoke();
        }

        /// <summary>
        /// Coroutine fading in.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        private IEnumerator FadeInRoutine(float time)
        {
            OnFadeInStart?.Invoke();

            float startAlpha = 0f; // Alpha is 0 because the FadeIn start with a DrawTexture invisible
            float progress = 0f;

            while (progress < 1f)
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime / time);
                _alpha = Mathf.Lerp(startAlpha, 1f, progress);
                yield return null;
            }

            OnFadeInEnd?.Invoke();
        }

        private void OnGUI()
        {
            GUI.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, _alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }
}