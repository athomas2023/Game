using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBookAnim : MonoBehaviour
{
    public GameObject PanelMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            ShowHideMenu();
        }
    }
    public void ShowHideMenu()
    {
        if(PanelMenu !=null)
        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}
