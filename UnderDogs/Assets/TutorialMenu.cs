using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialMenu : MonoBehaviour
{
    public InputActionReference nextHint;
    public InputActionReference returnButton;

    int hintPage = 0;

    [Header("UI")]
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject tutorialMenuUI;
    [SerializeField] GameObject cogUIHints;
    [SerializeField] GameObject bridgeUIHints;
    [SerializeField] GameObject elevatorUIHints;
    [SerializeField] GameObject robitsUIHints;

    void Start()
    {
        nextHint.action.performed += NextHint_Performed;
        returnButton.action.performed += Return_Performed;
    }

    private void OnEnable()
    {
        hintPage = 0;
    }

    private void Return_Performed(InputAction.CallbackContext obj)
    {
        Return();
    }

    private void NextHint_Performed(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.GameIsPaused)
        {
            hintPage++;
            switch (hintPage)
            {
                case 1:
                    tutorialMenuUI.SetActive(false);
                    cogUIHints.SetActive(true);
                    break;

                case 2:
                    cogUIHints.SetActive(false);
                    bridgeUIHints.SetActive(true);
                    break;

                case 3:
                    bridgeUIHints.SetActive(false);
                    elevatorUIHints.SetActive(true);
                    break;

                case 4:
                    elevatorUIHints.SetActive(false);
                    robitsUIHints.SetActive(true);
                    break;
            }

            if (hintPage >= 6) { Return(); }
        }
    }

    void Return()
    {

        elevatorUIHints.SetActive(false);
        bridgeUIHints.SetActive(false);
        cogUIHints.SetActive(false);
        robitsUIHints.SetActive(false);

        pauseMenuUI.SetActive(true);
        hintPage = 0;
    }
}
