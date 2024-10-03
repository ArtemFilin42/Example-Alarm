using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using ExampleClock.Scripts.Services;

namespace ExampleClock.Scripts.Widgets
{
    [System.Serializable]
    public struct ClockServer
    {
        public string Name;
        public string Url;
    }

    public class ClockWidget : MonoBehaviour
    {
        //====================================
        //======Vars
        //====================================

        [SerializeField] private TMP_Dropdown      clockServerDropdown;
        [SerializeField] private List<ClockServer> clockServerList;
        [SerializeField] private GameObject        clockSecondsArrow;
        [SerializeField] private GameObject        clockMinutesArrow;
        [SerializeField] private GameObject        clockHoursArrow;
        private ClockServer clockServer;
        private DateTime    clockTime;

        //====================================
        //======MonoBehaviour
        //====================================

        private void Awake()
        {
            LoadServerDropdown();
        }

        //====================================
        //======UI
        //====================================

        private void UpdateServerDropdown()
        {
            //Dropdown
            clockServerDropdown.ClearOptions();
            var names = new List<string>();
            foreach (var server in clockServerList)
            {
                names.Add(server.Name);
            }
            clockServerDropdown.AddOptions(names);
        }

        //====================================
        //======Clock
        //====================================

        private void LoadClockServer()
        {
            var index = clockServerDropdown.value;
            clockServer = clockServerList[index];
        }

        private void LoadClockTime()
        {
            clockTime = NetworkTimeService.RequestTime(clockServer.Url);
        }

        //====================================
        //======Events
        //====================================

        private void OnServerSelect()
        {
            LoadClockServer();
        }

    }
}