using System;

namespace Assets.PixelCrew.Components.Model
{
    [Serializable]
    public class PlayerData
    {
        public int Coins;
        public int Hp;
        public bool IsArmed;

        public PlayerData()
        {
            Coins = 0;
            Hp = 0;
            IsArmed = false;
        }

        public PlayerData(int coins, int hp, bool isArmed)
        {
            Coins = coins;
            Hp = hp;
            IsArmed = isArmed;
        }

        public void Copy(PlayerData data) 
        {
            this.Coins = data.Coins;
            this.Hp = data.Hp;
            this.IsArmed = data.IsArmed;
        }
    }
}
