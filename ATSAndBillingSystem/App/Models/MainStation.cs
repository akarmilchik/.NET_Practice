using ATS.App.Interfaces;
using ATS.App.Requests;
using System;
using System.Collections.Generic;

namespace ATS.App.Models
{
    class MainStation : Station
    {
        public MainStation(ICollection<ITerminal> terminalCollection, ICollection<IPort> portCollection) : base(terminalCollection, portCollection)
        {

        }

        public void OnOutgoingRequest(object sender, Request request)
        {
            if (request.GetType() == typeof(OutgoingRequest))
            {
                RegisterOutgoingRequest(request as OutgoingRequest);
            }
        }

        public override void RegisterEventHandlersForPort(IPort port)
        {
            port.StateChanged += (sender, state) => { Console.WriteLine("Station detected the port changed its State to {0}", state); };
        }

        public override void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.OutgoingConnection += this.OnOutgoingRequest;
            terminal.IncomingRespond += OnIncomingCallRespond;
        }
    }
}
