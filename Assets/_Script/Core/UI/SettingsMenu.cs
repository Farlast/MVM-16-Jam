using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Script.Core
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Dropdown resolutionDropdown;

        Resolution[] resolutions;
        
        private void Start()
        {
            if (resolutionDropdown != null)
                AddDropdown();
        }
       
        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", volume);
        }
        public void SetSFXVolume(float volume)
        {
            audioMixer.SetFloat("SFXVolume", volume);
        }
        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", volume);
        }
        public void Fullscreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }
        public void FullscreenMode(int index)
        {
            //Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        }
        void AddDropdown()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = "";
                
                if (resolutions[i].refreshRate > 0)
                 option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
                else
                 option = resolutions[i].width + "x" + resolutions[i].height;
                
                options.Add(option);

                if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
        public void SetResolution(int index)
        {
            Resolution resolution = resolutions[index];
            Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
        }
    }
}
