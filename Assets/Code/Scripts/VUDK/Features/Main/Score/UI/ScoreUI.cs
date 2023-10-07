namespace VUDK.Features.Main.Score.UI
{
    using UnityEngine;
    using TMPro;
    using VUDK.Features.Main.EventsSystem;
    using VUDK.Features.Main.EventsSystem.Events;
    using VUDK.Generic.Managers;

    public class ScoreUI : MonoBehaviour
    {
        [SerializeField, Header("Incipits")]
        private string _incipitScore;
        [SerializeField]
        private string _incipitHighScore;

        [SerializeField, Header("Texts")]
        private TMP_Text _scoreText;
        [SerializeField]
        private TMP_Text _highscoreText;

        private void OnEnable()
        {
            GameManager.Instance.EventManager.AddListener<int>(EventKeys.ScoreEvents.OnScoreChange, UpdateScoreText);
            GameManager.Instance.EventManager.AddListener<int>(EventKeys.ScoreEvents.OnHighScoreChange, UpdateHighScoreText);
        }

        private void OnDisable()
        {
            GameManager.Instance.EventManager.RemoveListener<int>(EventKeys.ScoreEvents.OnScoreChange, UpdateScoreText);
            GameManager.Instance.EventManager.RemoveListener<int>(EventKeys.ScoreEvents.OnHighScoreChange, UpdateHighScoreText);
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = _incipitScore + score.ToString();
        }

        private void UpdateHighScoreText(int highScore)
        {
            _highscoreText.text = _incipitHighScore + highScore.ToString();
        }
    }
}