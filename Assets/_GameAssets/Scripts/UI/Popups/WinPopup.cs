using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TimerUI _timerUI;
    [SerializeField] Button _oneMoreButton;
    [SerializeField] Button _mainMenuButton;
    [SerializeField] TMP_Text _timerText;






    public void OnOneMoreButtonClicked()
    {
        SceneManager.LoadScene(Consts.Scene_Names.GAME_MAIN_SCENE); 
    }

    public void OnMainMenuButtonClicked()
    {

    }

    private void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();
        _oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }
}
