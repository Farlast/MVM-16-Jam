namespace Script.Player
{
    public abstract class Weapon
    {
       
        public float Damage { get; protected set; }
        public float KnockBackValue { get; protected set; }
        public int Maxcombo { get; protected set; }

        protected PlayerBase playerBase;
        protected Weapon(float damage, PlayerBase playerBase)
        {
            Damage = damage;
            this.playerBase = playerBase;
        }

        public abstract void Attack(int currentCombo,float attackDirection);
      
    }
}
