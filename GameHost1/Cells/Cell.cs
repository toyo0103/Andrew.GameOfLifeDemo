using System;
namespace GameHost1.Cells
{
    public class Cell
    {
        public CellStateEnum State{get; private set;}
        public Cell(CellStateEnum initState)
        {
            State = initState;

            // Create a timer and set a two second interval.
            var lifeTime = new System.Timers.Timer();
            lifeTime.Interval = new Random().Next(2000,10000);
            //lifeTime.Interval = 2000;
            // Hook up the Elapsed event for the timer. 
            lifeTime.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            lifeTime.AutoReset = true;

            // Start the timer
            lifeTime.Enabled = true;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            int aliveCellAroundMe = 0;

            foreach(ConnectionTunnelDelegate tunnel in CallTunnel.GetInvocationList())
            {
                if(string.IsNullOrEmpty(tunnel.Invoke()) == false) aliveCellAroundMe ++;
            }
            
            if(this.State == CellStateEnum.Alive)
            {
                if(aliveCellAroundMe < 2){ this.State = CellStateEnum.Dead; return;}
                if(aliveCellAroundMe ==2 || aliveCellAroundMe == 3){ this.State = CellStateEnum.Alive; return;}
                if(aliveCellAroundMe > 3){ this.State = CellStateEnum.Dead; return;}
            }

            this.State = aliveCellAroundMe == 3 ? CellStateEnum.Alive : CellStateEnum.Dead;
        }

        public string Echo()
        {
            return State == CellStateEnum.Alive ? "hi" : string.Empty; 
        }

        public delegate string ConnectionTunnelDelegate();
        public ConnectionTunnelDelegate CallTunnel;

        // public void Connect(ConnectionTunnelDelegate requestTunnel,ConnectionTunnelDelegate responseTunnel)
        // {
        //     //互相建立連線
        //     requestTunnel += Echo;
        //     CallTunnel += responseTunnel;
        // }

        public void Connect(Cell relateCell)
        {
            //互相建立連線
            relateCell.CallTunnel += Echo;
            CallTunnel += relateCell.Echo;
        }
    }
}