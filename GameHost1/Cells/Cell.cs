using System;
using System.Collections.Generic;
using System.Linq;

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
            var echo_count = 0;
            foreach(ＨowlDelegate howl in Howl.GetInvocationList())
            {
                if(string.IsNullOrEmpty(howl.Invoke()) == false) echo_count ++;
            }

            // foreach(ConnectionTunnelDelegate tunnel in CallTunnel.GetInvocationList())
            // {
            //     if(string.IsNullOrEmpty(tunnel.Invoke()) == false) aliveCellAroundMe ++;
            // }
            
            if(this.State == CellStateEnum.Alive)
            {
                if(echo_count < 2){ this.State = CellStateEnum.Dead; return;}
                if(echo_count ==2 || echo_count == 3){ this.State = CellStateEnum.Alive; return;}
                if(echo_count > 3){ this.State = CellStateEnum.Dead; return;}
            }

            this.State = echo_count == 3 ? CellStateEnum.Alive : CellStateEnum.Dead;
        }

        public string Response()
        {
            return State == CellStateEnum.Alive ? "hi" : string.Empty; 
        }

        // public delegate string ConnectionTunnelDelegate();
        // public ConnectionTunnelDelegate CallTunnel;

        // public void Connect(Cell relateCell)
        // {
        //     //互相建立連線
        //     relateCell.CallTunnel += Echo;
        //     CallTunnel += relateCell.Echo;
        // }

        public delegate string ＨowlDelegate();
        public ＨowlDelegate Howl;
    }
}