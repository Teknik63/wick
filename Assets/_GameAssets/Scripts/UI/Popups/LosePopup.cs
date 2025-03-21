using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TimerUI _timerUI;
    [SerializeField] Button _tryAgainButton;
    [SerializeField] Button _mainMenuButton;
    [SerializeField] TMP_Text _timerText;



    public void OnOneMoreButtonClicked()
    {
        SceneManager.LoadScene(Consts.Scene_Names.GAME_MAIN_SCENE);
    }

    public void OnTryAgainButtonClicked()
    {

    }

    private void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();
        _tryAgainButton.onClick.AddListener(OnOneMoreButtonClicked);
        _mainMenuButton.onClick.AddListener(OnTryAgainButtonClicked);
    }
}
