using UnityEngine;
using System.Collections;

namespace Krivodeling.UI.Effects.Examples
{
    public class MovingImage : MonoBehaviour
    {
        #region Variables
        private new RectTransform transform;

        [SerializeField] private float minX = -570f, maxX = 570f, minY = -300f, maxY = 300f;
        [Space]
        [SerializeField] private float minTime = 3f;
        [SerializeField] private float maxTime = 10f;
        [Space]
        [SerializeField] private float speed = 0.5f;

        private Vector2 target;
        #endregion

        #region Methods
        private void Start()
        {
            transform = GetComponent<RectTransform>();

            StartCoroutine(SetTarget());
        }

        private void Update()
        {
            transform.anchoredPosition = Vector2.Lerp(transform.anchoredPosition, target, speed * Time.deltaTime);
        }

        private IEnumerator SetTarget()
        {
            target = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            StartCoroutine(SetTarget());
        }
        #endregion
    }
}
