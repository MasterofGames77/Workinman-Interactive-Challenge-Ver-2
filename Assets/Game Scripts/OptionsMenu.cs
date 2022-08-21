using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullScreenTog, vsyncTog;

    public List<ResolutionItem> resolutions = new List<ResolutionItem>();

    private int selectedResolution;

    public TMP_Text resolutionLabel;

    // Start is called before the first frame update
    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        bool foundResolution = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundResolution = true;

                selectedResolution = i;

                UpdateResLabel();
            }
        }

        if (!foundResolution)
        {
            ResolutionItem newResolution = new ResolutionItem();
            newResolution.horizontal = Screen.width;
            newResolution.vertical = Screen.height;

            resolutions.Add(newResolution);
            selectedResolution = resolutions.Count - 1;

            UpdateResLabel();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }

        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        Screen.fullScreen = fullScreenTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
            Debug.Log("Saved Graphic Settings");
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Debug.Log("Saved Graphic Settings");
        }

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullScreenTog.isOn);
    }
}


[System.Serializable]
public class ResolutionItem
{
    public int horizontal, vertical;
}