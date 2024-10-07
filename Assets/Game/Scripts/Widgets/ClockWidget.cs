using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using ExampleClock.Scripts.Services;
using DG.Tweening;
using ExampleClock.Scripts.Consts;
using System.Text;

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
        [SerializeField] private GameObject        clockHoursArrow;
        [SerializeField] private GameObject        clockMinutesArrow;
        [SerializeField] private GameObject        clockSecondsArrow;
        [SerializeField] private TMP_Text          clockTimeLabel;
        //
        private ClockServer clockServer;
        private DateTime    clockTime;
        //
        private float       clockLocalTimer  = 0f;
        private float       clockLocalPeriod = 1f;

        //====================================
        //======MonoBehaviour
        //====================================

        private void Awake()
        {
            UpdateServerDropdown();
            LoadClockServer();
            LoadClockTime();
            UpdateTimeDisplay(false);
        }

        private void FixedUpdate()
        {
            clockLocalTimer += Time.deltaTime;
            if(clockLocalTimer >= clockLocalPeriod)
            {
                clockLocalTimer = 0f;
                clockTime = clockTime.AddSeconds(1);
                UpdateTimeDisplay(true);
            }
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

        private void UpdateTimeDisplay(bool animated)
        {
            //Time
            int hours = clockTime.Hour;
            float hoursRotation = -360/12 * (hours % 12);
            int minutes = clockTime.Minute;
            float minutesRotation = -360/60 * minutes;
            int seconds = clockTime.Second;
            float secondsRotation = -360/60 * seconds;

            //Visual
            if (animated)
            {
                clockHoursArrow.gameObject.transform.DORotate(new Vector3(0, 0, hoursRotation), ClockConsts.RotationDuration);
                clockMinutesArrow.gameObject.transform.DORotate(new Vector3(0, 0, minutesRotation), ClockConsts.RotationDuration);
                clockSecondsArrow.gameObject.transform.DORotate(new Vector3(0, 0, secondsRotation), ClockConsts.RotationDuration);
            }
            else
            {
                clockHoursArrow.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, hoursRotation));
                clockMinutesArrow.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, minutesRotation));
                clockSecondsArrow.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, secondsRotation));
            }

            //Label
            StringBuilder time = new StringBuilder();
            time.Append(string.Format("{0:00}", hours));
            time.Append(":");
            time.Append(string.Format("{0:00}", minutes));
            time.Append(":");
            time.Append(string.Format("{0:00}", seconds));
            clockTimeLabel.text = time.ToString();                
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

        public void OnServerSelect()
        {
            LoadClockServer();
            LoadClockTime();
        }

    }
}