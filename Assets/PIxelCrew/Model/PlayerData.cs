using System;

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

        public void Copy(PlayerData data) 
        {
            this.Coins = data.Coins;
            this.Hp = data.Hp;
            this.IsArmed = data.IsArmed;
            this.Swords = data.Swords;
        }
    }
}
