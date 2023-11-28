using System;
using UnityEngine;

namespace PixelCrew.Components.Model
{
    [Serializable]
    public class PlayerData
    {
        public int Coins;
        public int Hp;
        public bool IsArmed;
        public int Swords;

        public PlayerData()
        {
            Coins = 0;
            Hp = 0;
            IsArmed = false;
            Swords = 0;
        }

        public PlayerData(int coins, int hp, bool isArmed, int swords)
        {
            Coins = coins;
            Hp = hp;
            IsArmed = isArmed;
            Swords = swords;
        }

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);            
        }
    }
}
