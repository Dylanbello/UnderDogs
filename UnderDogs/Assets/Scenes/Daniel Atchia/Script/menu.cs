using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class menu : MonoBehaviour
{
    public TextMeshProUGUI option1;
    public TextMeshProUGUI option2;
    public TextMeshProUGUI option3;
    public TextMeshProUGUI option4;
    public InputActionReference navigation;
    public InputActionReference click;
    public Vector2 navInput;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Canvas loadingScreen;
    public Canvas credits;
    private int numberOfOptions = 4;

    private int selectedOption;

    // Use this for initialization
    void Start()
    {
        selectedOption = 1;
        option1.color = new Color32(255, 255, 255, 255);
        image1.color = new Color32(0, 0, 0, 100);
        option2.color = new Color32(0, 0, 0, 255);
        image2.color = new Color32(0, 0, 0, 0);
        option3.color = new Color32(0, 0, 0, 255);
        image3.color = new Color32(0, 0, 0, 0);
        option4.color = new Color32(0, 0, 0, 255);
        image4.color = new Color32(0, 0, 0, 0);

        click.action.performed += Action_performed;
        navigation.action.canceled += Action_canceled;
        navigation.action.performed += Action_performed1;
    }

    private void Action_canceled(InputAction.CallbackContext obj)
    {
        navInput.y = 0;
        Debug.Log("nav");
    }

    private void Action_performed1(InputAction.CallbackContext obj)
    {
        navInput = obj.ReadValue<Vector2>();
        updateNav();
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Picked: " + selectedOption); //For testing as the switch statment does nothing right now.

        switch (selectedOption) //Set the visual indicator for which option you are on.
        {
            case 1:
                //SceneManager.LoadScene("MasterScene");
                loadingScreen.enabled = true;
                LevelManager.Instance.LoadScene();
                break;
            case 2:
                /*Do option two*/
                break;
            case 3:
                Application.Quit();
                break;
        }
    }

    // Update is called once per frame
    void updateNav()
    {
        if (navInput.y < 0)
        { //Input telling it to go up or down.
            selectedOption += 1;
            if (selectedOption > numberOfOptions) //If at end of list go back to top
            {
                selectedOption = 1;
            }

            option1.color = new Color32(0, 0, 0, 255); //Make sure all others will be black (or do any visual you want to use to indicate this)
            option2.color = new Color32(0, 0, 0, 255);
            option3.color = new Color32(0, 0, 0, 255);
            option4.color = new Color32(0, 0, 0, 255);
            image1.color = new Color32(0, 0, 0, 0);
            image2.color = new Color32(0, 0, 0, 0);
            image3.color = new Color32(0, 0, 0, 0);
            image4.color = new Color32(0, 0, 0, 0);
            switch (selectedOption) //Set the visual indicator for which option you are on.
            {
                case 1:
                    option1.color = new Color32(255, 255, 255, 255);
                    image1.color = new Color32(0, 0, 0, 100);
                    break;
                case 2:
                    option2.color = new Color32(255, 255, 255, 255);
                    image2.color = new Color32(0, 0, 0, 100);
                    break;
                case 3:
                    option3.color = new Color32(255, 255, 255, 255);
                    image3.color = new Color32(0, 0, 0, 100);
                    break;
                case 4:
                    option4.color = new Color32(255, 255, 255, 255);
                    image4.color = new Color32(0, 0, 0, 100);
                    break;
            }
        }

        if (navInput.y > 0)
        { //Input telling it to go up or down.
            selectedOption -= 1;
            if (selectedOption < 1) //If at end of list go back to top
            {
                selectedOption = numberOfOptions;
            }

            option1.color = new Color32(0, 0, 0, 255); //Make sure all others will be black (or do any visual you want to use to indicate this)
            option2.color = new Color32(0, 0, 0, 255);
            option3.color = new Color32(0, 0, 0, 255);
            option4.color = new Color32(0, 0, 0, 255);
            image1.color = new Color32(0, 0, 0, 0);
            image2.color = new Color32(0, 0, 0, 0);
            image3.color = new Color32(0, 0, 0, 0);
            image4.color = new Color32(0, 0, 0, 0);

            switch (selectedOption) //Set the visual indicator for which option you are on.
            {
                case 1:
                    option1.color = new Color32(255, 255, 255, 255);
                    image1.color = new Color32(0, 0, 0, 100);
                    break;
                case 2:
                    option2.color = new Color32(255, 255, 255, 255);
                    image2.color = new Color32(0, 0, 0, 100);
                    break;
                case 3:
                    option3.color = new Color32(255, 255, 255, 255);
                    image3.color = new Color32(0, 0, 0, 100);
                    break;
                case 4:
                    option4.color = new Color32(255, 255, 255, 255);
                    image4.color = new Color32(0, 0, 0, 100);
                    break;
            }
        }
    }
}
