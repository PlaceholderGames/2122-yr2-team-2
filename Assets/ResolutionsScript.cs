using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ResolutionsScript : MonoBehaviour
{
    GameObject dropdownMenu = null;
    public Dropdown resolutionsDropdown;


    Resolution[] resolutions;

    // Start is called before the first frame update
    // This script adds all available resolutions to the resolutions dropdown, then sets the screen resolution to the selected option.
    void Start()
    {
        dropdownMenu = GameObject.Find("ResDropdown");
        resolutionsDropdown = dropdownMenu.GetComponent<Dropdown>();

        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionsDropdown.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));

            resolutionsDropdown.value = i;

            resolutionsDropdown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[resolutionsDropdown.value].width, resolutions[resolutionsDropdown.value].height, true); });
        }
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
