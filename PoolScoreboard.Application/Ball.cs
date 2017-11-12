using System;

namespace PoolScoreboard.Application
{
    public interface IBall
    {
        string Identifier { get; }
        bool OnTable { get; set; }
        ITeam SunkBy { get; set; }
        bool LegallySunk { get; set; }
        
        void Sink();
    }
    
    public class PoolBall : IBall
    {
        public string Identifier { get; }
        public bool OnTable { get; set; }
        public ITeam SunkBy { get; set; }
        public bool LegallySunk { get; set; }
        
        
        private int Number => int.Parse(Identifier);
        public BallClass Class
        {
            get
            {
                if (Number < 8) return BallClass.Solids;
                return Number > 8 ? BallClass.Stripes : BallClass.EightBall;
            }
        }
        
        public PoolBall(int number)
        {
            if (number < 1 || number > 15) throw new ArgumentException("Ball number is out of range.");
            
            Identifier = number.ToString();
            OnTable = true;
            LegallySunk = false;
            SunkBy = null;
        }
        
        public void Sink()
        {
            SunkBy = Table.CurrentShooter;
            OnTable = false;
        }
    }
    
    public enum BallClass
    {
        Solids,
        Stripes,
        EightBall
    }
}