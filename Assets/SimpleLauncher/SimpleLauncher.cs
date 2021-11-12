using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleLauncher : MonoBehaviour
{
    public EventSystem EvtSys;
    public GameObject Panel;
    public Dropdown ResolutionDropdown;
    public Toggle FullscreenToggle;
    public Dropdown QualitySettingsDropdown;
    public Toggle SaveSettingToggle;
    public Button LaunchButton;
    
    private void Start()
    {
        //we skip the editor as it seems it won't register input fast enough to allow keeping submit pressed
        //In editor you should nearly never go through that screen anyway, just useful to debug/personalize that screen
        if (!Application.isEditor && !Input.GetButton("Submit") && File.Exists(Application.persistentDataPath + "/LauncherData"))
        {
            using var reader =
                new BinaryReader(new FileStream(Application.persistentDataPath + "/LauncherData", FileMode.Open));
            int w = reader.ReadInt32();
            int h = reader.ReadInt32();
            bool f = reader.ReadBoolean();
            int qualityLevel = reader.ReadInt32();
                
            Screen.SetResolution(w,h,f);
            QualitySettings.SetQualityLevel(qualityLevel);
            SceneManager.LoadScene(1, LoadSceneMode.Single);

            return;
        }

        Panel.SetActive(false);
        Screen.SetResolution(1024, 768, false);

        //Force select the Resolution dropdown to allow to handle it with pads/keyboard
        EvtSys.SetSelectedGameObject(null);
        EvtSys.SetSelectedGameObject(ResolutionDropdown.gameObject);

        List<Dropdown.OptionData> resolutionDropdownOptions = new List<Dropdown.OptionData>();
        List<Resolution> alreadyAddedResolutions = new List<Resolution>();
        foreach (var resolution in Screen.resolutions)
        {
            //to simplify the number of offered resolution, we just only add one per w/h ignore refresh rate.
            if(alreadyAddedResolutions.FindIndex(existingRes => existingRes.width == resolution.width
                                                                && existingRes.height == resolution.height) != -1)
                continue;
            
            resolutionDropdownOptions.Add(new Dropdown.OptionData($"{resolution.width}x{resolution.height}"));
            alreadyAddedResolutions.Add(resolution);
        }
        ResolutionDropdown.options = resolutionDropdownOptions;
        ResolutionDropdown.value = ResolutionDropdown.options.Count - 1;

        FullscreenToggle.isOn = true;
        
        List<Dropdown.OptionData> qualityOptions = new List<Dropdown.OptionData>();
        foreach (var name in QualitySettings.names)
        {
            qualityOptions.Add(new Dropdown.OptionData(name));
        }
        QualitySettingsDropdown.options = qualityOptions;

        LaunchButton.onClick.AddListener(() =>
        {
            var selectedResolution = Screen.resolutions[ResolutionDropdown.value];

            if (SaveSettingToggle.isOn)
            {
                using var writer = new BinaryWriter(new FileStream(Application.persistentDataPath + "/LauncherData",
                    FileMode.Create));
                writer.Write(selectedResolution.width);
                writer.Write(selectedResolution.height);
                writer.Write(FullscreenToggle.isOn);
                writer.Write(QualitySettingsDropdown.value);
                writer.Close();
                writer.Dispose();
            }
            
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, FullscreenToggle.isOn);
            QualitySettings.SetQualityLevel(QualitySettingsDropdown.value);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        });
    }
}