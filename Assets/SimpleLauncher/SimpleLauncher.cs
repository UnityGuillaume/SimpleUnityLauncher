using System.Collections.Generic;
using System.IO;
using UnityEditor.UI;
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
        if (!Input.GetButton("Submit") && File.Exists(Application.persistentDataPath + "/LauncherData"))
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

        EvtSys.SetSelectedGameObject(null);
        EvtSys.SetSelectedGameObject(ResolutionDropdown.gameObject);

        List<Dropdown.OptionData> resolutionDropdownOptions = new List<Dropdown.OptionData>();
        foreach (var resolution in Screen.resolutions)
        {
            resolutionDropdownOptions.Add(new Dropdown.OptionData(resolution.ToString()));
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