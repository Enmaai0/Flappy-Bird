using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("UI面板")]
    public GameObject readyPanel;
    public GameObject gamePanel;
    public GameObject gameOverPanel;

    [Header("UI按钮")]
    public Button startButton;

    [Header("动画设置")]
    public float fadeDuration = 0.5f;


    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        if (gameOverPanel.GetComponent<CanvasGroup>() == null)
        {
            gameOverPanel.AddComponent<CanvasGroup>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeState(GameState newState)
    {
        readyPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // 根据状态打开对应的Panel
        switch (newState)
        {
            case GameState.Ready:
                readyPanel.SetActive(true);
                break;
                
            case GameState.Playing:
                gamePanel.SetActive(true);
                break;
                
            case GameState.GameOver:
                StartCoroutine(ShowGameOverPanelWithFade());
                break;
        }
    }

    IEnumerator ShowGameOverPanelWithFade()
    {
        gameOverPanel.SetActive(true);
        CanvasGroup canvasGroup = gameOverPanel.GetComponent<CanvasGroup>();
        
        // 从透明开始
        canvasGroup.alpha = 0f;
        
        // 渐入动画
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }
        
        canvasGroup.alpha = 1f;
    }

    void OnStartButtonClick()
    {
        GameManager.Instance.StartGame();
    }
}
