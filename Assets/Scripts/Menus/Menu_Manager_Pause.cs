using System.Linq;
using UnityEngine;

public class Menu_Manager_Pause : Menu_Manager
{
    private void Start()
    {
        foreach (var item in screenDatas)
            item.ScreenRoot.SetActive(false);

        screenDatas[_startScreen].ScreenRoot.SetActive(true);

        currentScreen = _startScreen;
        _eventSystem.SetSelectedGameObject(screenDatas[currentScreen].EnterButton.gameObject);


        _canvas.gameObject.SetActive(false);
        GameManager.GameLogic.onGameResume += ClosePauseMenu;
        GameManager.GameLogic.onGamePause += ShowPauseMenu;
    }

    private void OnEnable()
    {
        GameManager.GameLogic.onGameResume += ClosePauseMenu;
        GameManager.GameLogic.onGamePause += ShowPauseMenu;
    }

    private void OnDisable()
    {
        GameManager.GameLogic.onGameResume -= ClosePauseMenu;
        GameManager.GameLogic.onGamePause -= ShowPauseMenu;
    }

#if UNITY_EDITOR

    protected override void Validation()
    {
        base.Validation();

        screenDatas = FindObjectsByType<MenuScreenContent>(FindObjectsSortMode.InstanceID);

        // Filter to only MenuScreenContent_Pause and sort by PauseMenuScreenTypes order
        var sorted = screenDatas
            .OfType<MenuScreenContent_Pause>()
            .OrderBy(s => s.MainScreenTypes)
            .Cast<MenuScreenContent>()
            .ToArray();

        screenDatas = sorted;

        if (!_audioSource) _audioSource = GetComponent<AudioSource>();
    }
#endif
    public void Resume()
    {
        GameManager.GameLogic.SetPauseState(false);
        ClosePauseMenu();
    }

    public void QuitToMenu()
    {
        // TODO: Require Scene Manager
        //GameManager.SceneManager.LoadScene(MainSceneManager.GameScenes.MainMenu);
        print("Quit to Menu...");
    }

    void ShowPauseMenu()
    {
        _canvas.gameObject.SetActive(true);
        
    }

    void ClosePauseMenu()
    {
        _canvas.gameObject.SetActive(false);
    }

    protected override void ToggleScreen(int openingScreen, int closingScreen)
    {
        base.ToggleScreen(openingScreen, closingScreen);

        // Disable closing and Enable opening additional screen content
        foreach (var item in screenDatas[openingScreen].AdditionalScreenContent) { item.SetActive(true); }
        foreach (var item in screenDatas[closingScreen].AdditionalScreenContent) { item.SetActive(false); }

        MenuScreenContent OpeningScreen = screenDatas[openingScreen];
        MenuScreenContent ClosingScreen = screenDatas[closingScreen];

        // Disable closing scene and enable opening screen
        OpeningScreen.ScreenRoot.SetActive(true);
        ClosingScreen.ScreenRoot.SetActive(false);

        /* // Optionally choose to determine if the pages main button should be selected if using Keyboard or Gamepad controls
        if (// Insert check for player input here)
        {
            
        }
        */

        // Select the opening screen
        currentScreen = openingScreen;

        if (ClosingScreen.UseExitButton) _eventSystem.SetSelectedGameObject(ClosingScreen.ExitButton.gameObject);
        else _eventSystem.SetSelectedGameObject(OpeningScreen.EnterButton.gameObject);
    }
}
