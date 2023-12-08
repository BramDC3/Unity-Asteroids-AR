using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameState gameState;
    [SerializeField] private Slider playerHealth;
    [SerializeField] private Slider planetHealth;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text finalScoreText;

    private void OnEnable()
    {
        gameState.OnIncreaseScore.AddListener(UpdateScoreUI);
        gameState.OnGameOver.AddListener(ShowGameOverScreen);
    }
    private void OnDisable()
    {
        gameState.OnIncreaseScore.RemoveListener(UpdateScoreUI);
        gameState.OnGameOver.RemoveListener(ShowGameOverScreen);
    }
    
    public void UpdateScoreUI(int newScore)
    {
        scoreText.text = $"Score {newScore}";
    }

    public void UpdatePlayerHealthUI(int newHealth)
    {
        playerHealth.value = newHealth;
    }
    
    public void UpdatePlanetHealthUI(int newHealth)
    {
        planetHealth.value = newHealth;
    }
    
    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        finalScoreText.text = $"Score: {gameState.Score}";
    }
}
