using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<Button> pressableButtons;
    public int highlightedButton;
    bool axisInUse;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") == -1)
        {
            if (axisInUse == false)
            {
                highlightedButton += 1;
                if (highlightedButton > pressableButtons.Count - 1)
                {
                    highlightedButton = 0;
                }
                axisInUse = true;
            }   
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") == 1)
        {
            if (axisInUse == false)
            {
                highlightedButton -= 1;
                if (highlightedButton < 0)
                {
                    highlightedButton = pressableButtons.Count - 1;
                }
                axisInUse = true;
            }
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            axisInUse = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (pressableButtons[highlightedButton].gameObject.transform.parent.gameObject.activeSelf == true)
            {
                pressableButtons[highlightedButton].onClick.Invoke();
            }
        }

        UpdateButton();
    }

    private void UpdateButton()
    {
        for (int i = 0; i < pressableButtons.Count; i++)
        {
            pressableButtons[i].GetComponent<Image>().color = Color.white;
            if (i == highlightedButton)
            {
                pressableButtons[i].GetComponent<Image>().color = Color.cyan;
            }
        }

    }
}
