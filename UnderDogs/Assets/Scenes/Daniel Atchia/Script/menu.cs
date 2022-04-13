using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menu : MonoBehaviour
{

    public TextMeshProUGUI option1;
    public TextMeshProUGUI option2;
    public TextMeshProUGUI option3;
    public TextMeshProUGUI option4;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) /*|| Controller input*/)
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

        if (Input.GetKeyDown(KeyCode.UpArrow) /*|| Controller input*/)
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

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
        {
            Debug.Log("Picked: " + selectedOption); //For testing as the switch statment does nothing right now.

            switch (selectedOption) //Set the visual indicator for which option you are on.
            {
                case 1:
                    /*Do option one*/
                    break;
                case 2:
                    /*Do option two*/
                    break;
                case 3:
                    /*Do option two*/
                    break;
            }
        }
    }
}
