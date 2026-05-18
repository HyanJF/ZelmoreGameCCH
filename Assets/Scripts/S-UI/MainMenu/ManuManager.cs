using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Roots")]
    public GameObject mainMenuRoot;
    public GameObject optionsMenuRoot;
    public GameObject ratingsMenuRoot;
    public GameObject playMenuRoot;

    [Header("Animators")]
    public MainMenuAnimator mainMenu;
    public GenericMenuAnimator optionsMenu;
    public GenericMenuAnimator ratingsMenu;
    public GenericMenuAnimator playMenu;

    void Start()
    {
        mainMenuRoot.SetActive(true);
        optionsMenuRoot.SetActive(false);
        ratingsMenuRoot.SetActive(false);
        playMenuRoot.SetActive(false);

        mainMenu.PlayAnimation(MenuAnimationState.Start);
    }

    // MAIN → OPTIONS
    public void GoToOptions()
    {
        mainMenu.PlayChange(() =>
        {
            mainMenuRoot.SetActive(false);
            optionsMenuRoot.SetActive(true);

            optionsMenu.PlayEnter();
        });
    }

    // OPTIONS → MAIN
    public void BackToMainMenuFromOptions()
    {
        optionsMenu.PlayChange(() =>
        {
            optionsMenuRoot.SetActive(false);
            mainMenuRoot.SetActive(true);

            mainMenu.PlayAnimation(MenuAnimationState.Enter);
        });
    }

    // MAIN → RATINGS
    public void GoToRatings()
    {
        mainMenu.PlayChange(() =>
        {
            mainMenuRoot.SetActive(false);
            ratingsMenuRoot.SetActive(true);

            ratingsMenu.PlayEnter();
        });
    }

    // RATINGS → MAIN
    public void BackToMainMenuFromRatings()
    {
        ratingsMenu.PlayChange(() =>
        {
            ratingsMenuRoot.SetActive(false);
            mainMenuRoot.SetActive(true);

            mainMenu.PlayEnter();
        });
    }

    // MAIN -> PLAY
    public void GoToPlay()
    {
        mainMenu.PlayChange(() =>
        {
            mainMenuRoot.SetActive(false);
            playMenuRoot.SetActive(true);

            playMenu.PlayEnter();
        });
    }

    // PLAY -> MAIN
    public void BackToMainMenuFromPlay()
    {
        playMenu.PlayChange(() =>
        {
            mainMenuRoot.SetActive(true);
            playMenuRoot.SetActive(false);
            
            mainMenu.PlayEnter();
        });
    }

    // EXIT
    public void ExitGame()
    {
        mainMenu.PlayExit();
    }
}