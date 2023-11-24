using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<Button> pressableButtons;
    public int highlightedButton;
    bool axisInUse;
    bool newSceneClickDelay = true;

    private void OnEnable()
    {
        axisInUse = false;
        highlightedButton = 0;
        Invoke("EnableClicking", 1f);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Vertical") == -1)
        {
            if (axisInUse == false)
            {
                highlightedButton += 1;
                if (highlightedButton > pressableButtons.Count - 1)
                {
                    highlightedButton = 0;
                }
                axisInUse = true;
                StartCoroutine(SelectionReset());
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical") == 1)
        {
            if (axisInUse == false)
            {
                highlightedButton -= 1;
                if (highlightedButton < 0)
                {
                    highlightedButton = pressableButtons.Count - 1;
                }
                axisInUse = true;
                StartCoroutine(SelectionReset());
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (pressableButtons[highlightedButton].gameObject.transform.parent.gameObject.activeSelf == true && newSceneClickDelay == false)
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

    private IEnumerator SelectionReset()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        axisInUse = false;
        yield return null;
    }

    private void EnableClicking()
    {
        newSceneClickDelay = false;
    }
}
