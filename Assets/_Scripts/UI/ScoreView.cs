using TMPro;
using UnityEngine;

namespace Code
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;

        private int _score;

        private void OnEnable()
        {
            EventBus.Subscribe(EventType.Coin, UpdateView);
            EventBus.Subscribe(EventType.Dead, ResetView);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.Coin, UpdateView);
            EventBus.Unsubscribe(EventType.Dead, ResetView);
        }

        private void UpdateView()
        {
            _score++;
            _text.text = _score.ToString();
        }
        private void ResetView()
        {
            _score = 0;
            _text.text = _score.ToString();
        }
    }
}