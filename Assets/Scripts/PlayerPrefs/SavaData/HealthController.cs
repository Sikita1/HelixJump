﻿namespace SaveData
{
    [System.Serializable]
    public class HealthController
    {
        public int Health = 5;
    }

    [System.Serializable]
    public class TimerData
    {
        public int CurrentEnergy;

        public string NextEnergyTime;
        public string LastAddedTime;
    }
}