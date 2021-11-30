using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabbButton> TabbButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabbButton selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(TabbButton button)
    { 
        if(TabbButtons == null)
        {
            TabbButtons = new List<TabbButton>();
        }
    }
    public void OnTabEnter(TabbButton button)
    {
        ResetTabs();
        if(selectedTab == null || button !=selectedTab)
        {
            button.background.sprite = tabHover;
        }
        
    }

    public void OnTabExit(TabbButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabbButton button)
    {
        if(selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;

        selectedTab.Select();

        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for(int i=0; i<objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabbButton button in TabbButtons)
        {
           if(selectedTab!=null && button == selectedTab) { continue; }
            button.background.sprite = tabIdle;
        }
    }

}
