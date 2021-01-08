using ATS.DAL.Constants;
using ATS.DAL.Interfaces;
using ATS.DAL.Models.Requests;
using System;

namespace ATS.DAL.Models
{
    public class MainPort : Port
    {
        public MainPort()
        {
            this.StateChanged += (sender, state) => { Console.WriteLine("Port detected the State is changed to {0}", state); };
        }

        public void OnOutgoingCall(object sender, Request request)
        {
            if (request.GetType() == typeof(OutgoingRequest) && this.PortState == PortState.Free)
            {
                this.PortState = PortState.Calling;
            }
        }

        public override void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.OutgoingConnection += this.OnOutgoingCall;

            terminal.Online += (port, args) => { this.PortState = PortState.Free; };

            terminal.Offline += (port, args) => { this.PortState = PortState.Disabled; };
        }
    }
}
