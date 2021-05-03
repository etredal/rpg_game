using UnityEngine;

namespace Krivodeling.UI.Effects.Examples
{
    public class UIBlurWithCanvasGroup : MonoBehaviour
    {
        #region Variables
        private UIBlur uiblur;
        private CanvasGroup canvasGroup;
        #endregion

        #region Methods
        private void Start()
        {
            SetComponents();

            uiblur.onBeginBlur.AddListener(() => canvasGroup.blocksRaycasts = true);
            uiblur.onBlurChanged.AddListener(OnBlurChanged);
            uiblur.onEndBlur.AddListener(() => canvasGroup.blocksRaycasts = false);
        }

        private void SetComponents()
        {
            uiblur = GetComponent<UIBlur>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnBlurChanged(float value)
        {
            canvasGroup.alpha = value;
        }
        #endregion
    }
}
