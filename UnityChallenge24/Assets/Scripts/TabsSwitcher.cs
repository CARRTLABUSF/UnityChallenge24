using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple tab switcher.
/// </summary>
public class TabsSwitcher : MonoBehaviour
{
    [SerializeField] private int initialTab;
    [SerializeField] private GameObject[] tabs;

    /// <summary>
    /// Switches the tab
    /// </summary>
    /// <param name="tab">Tab to switch to</param>
    public void SwitchTab(int tab)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
            if (i == tab)
            {
                tabs[i].SetActive(true);
            }
        }
    }
}
