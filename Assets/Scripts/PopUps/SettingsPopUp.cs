using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PopUps
{
    public class SettingsPopUp: MonoBehaviour
    {
        [SerializeField]
        public Button DeleteButton;
        [SerializeField]
        public Slider Slider;

        public void DeleteSaveData()
        {
            Save.DeleteData();
        }

        private void Awake()
        {
            DeleteButton.gameObject.SetActive(SceneManager.GetActiveScene().name == "MainMenu");

            var exists = Save.LoadDirectly().TryGetValue("volume", out var value);
            if (exists)
            {
                var a = JsonConvert.DeserializeObject<float>(value.ToString());
                Slider.value = a;
                AudioListener.volume = a;
            }
            else
            {
                ChangeVolume(1.0f);
            }
        }

        public void ChangeVolume(float volume)
        {
            AudioListener.volume = volume;
            var a = new Dictionary<string, object>
            {
                ["volume"] = volume
            };
            Save.SaveData(a);
        }
    }
}