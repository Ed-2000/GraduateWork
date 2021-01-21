using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _deadPanel;
    [SerializeField] private GameObject _maxSpeedPanel;
    [SerializeField] private GameObject _scoreMoveTextGO;
    [SerializeField] private Text _scoreText;

    private Text _scoreMoveText;

    private void Start()
    {
        if (_deadPanel.activeSelf)
        {
            _deadPanel.SetActive(false);
        }

        _scoreMoveText = _scoreMoveTextGO.GetComponent<Text>();
    }

    private void OnEnable()
    {
        CollisionWithObstacle.OnDidNotOvercameObstacle += ActiveDeadMenu;
        PlayerData.OnScoreСhanged += DrawScore;
        PlayerData.OnMaxSpeed += DrawMaxSpeed;
        PlayerData.OnScoreСhanged += MoveScoreTextAnim;
    }

    private void OnDisable()
    {
        CollisionWithObstacle.OnDidNotOvercameObstacle -= ActiveDeadMenu;
        PlayerData.OnScoreСhanged -= DrawScore;
        PlayerData.OnMaxSpeed -= DrawMaxSpeed;
        PlayerData.OnScoreСhanged -= MoveScoreTextAnim;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    private void ActiveDeadMenu()
    {
        _deadPanel.SetActive(true);
    }

    private void DrawScore()
    {
        _scoreText.text = "SCORE: " + PlayerData.Score.ToString();
    }

    private void DrawMaxSpeed()
    {
        _maxSpeedPanel.SetActive(true);
    }

    private void MoveScoreTextAnim()
    {
        _scoreMoveText.text = "+" + Score.addToScore.ToString();
        _scoreMoveTextGO.SetActive(true);
    }
}