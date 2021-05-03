using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace Krivodeling.UI.Effects
{
    public class UIBlur : MonoBehaviour
    {
        #region Variables
        private Material material;

        public Color color = Color.white;
        public float intensity { get => _intensity; set => _intensity = Mathf.Clamp01(value); }
        [SerializeField] [Range(0f, 1f)] private float _intensity;
        [Range(0f, 1f)] public float multiplier = 0.15f;

        [System.Serializable] public class BlurChangedEvent : UnityEvent<float> { }

        public UnityEvent onBeginBlur, onEndBlur;
        public BlurChangedEvent onBlurChanged = new BlurChangedEvent();
        #endregion

        #region Editor
#if UNITY_EDITOR
        private void OnValidate()
        {
            SetBlurInEditor();
        }

        private void SetBlurInEditor()
        {
            Material m = GetComponent<Image>().material;
            m.SetColor("_Color", color);
            m.SetFloat("_Intensity", intensity);
            m.SetFloat("_Multiplier", multiplier);
        }
#endif
        #endregion

        #region Methods
        private void Start()
        {
            SetComponents();
            SetBlur(color, intensity, multiplier);
        }

        private void SetComponents()
        {
            material = GetComponent<Image>().materialForRendering;
        }

        public void SetBlur(Color color, float intensity, float multiplier)
        {
            material.SetColor("_Color", color);
            material.SetFloat("_Intensity", intensity);
            material.SetFloat("_Multiplier", multiplier);
        }

        public void SetBlur(float value)
        {
            material.SetFloat("_Intensity", value);
        }

        public void BeginBlur(float speed)
        {
            StopAllCoroutines();
            StartCoroutine(BeginBlurCoroutine(speed));
        }

        private IEnumerator BeginBlurCoroutine(float speed)
        {
            onBeginBlur?.Invoke();

            while (intensity < 1f)
            {
                intensity += speed;
                SetBlur(intensity);
                onBlurChanged.Invoke(intensity);

                yield return null;
            }
        }

        public void EndBlur(float speed)
        {
            StopAllCoroutines();
            StartCoroutine(EndBlurCoroutine(speed));
        }

        private IEnumerator EndBlurCoroutine(float speed)
        {
            while (intensity > 0f)
            {
                intensity -= speed;
                SetBlur(intensity);
                onBlurChanged.Invoke(intensity);

                yield return null;
            }

            onEndBlur?.Invoke();
        }
        #endregion
    }
}
