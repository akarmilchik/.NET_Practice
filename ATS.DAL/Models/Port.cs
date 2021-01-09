using ATS.DAL.Constants;
using ATS.DAL.Interfaces;
using ATS.DAL.Models.Requests;
using System;

namespace ATS.DAL.Models
{
    public  class Port : IPort
    {
        private PortState _portState;

        public PortState PortState
        {
            get
            {
                return _portState;
            }
            set
            {
                if (_portState != value)
                {
                    OnStateChanging(this, value);

                    _portState = value;

                    OnStateChanged(this, _portState);
                }
            }
        }

        public Port()
        {
            this.StateChanged += (sender, state) => { Console.WriteLine("Port detected the State is changed to {0}", state); };
        }    

        public event EventHandler<PortState> StateChanging;

        public event EventHandler<PortState> StateChanged;

        protected virtual void OnStateChanged(object sender, PortState state)
        {
            if (StateChanged != null)
            {
                StateChanged(sender, state);
            }
        }

        protected virtual void OnStateChanging(object sender, PortState newState)
        {
            if (StateChanging != null)
            {
                StateChanging(sender, newState);
            }
        }

        public void OnOutgoingCall(object sender, Request request)
        {
            if (request.GetType() == typeof(OutgoingRequest) && this.PortState == PortState.Free)
            {
                this.PortState = PortState.Calling;
            }
        }


        public void ClearEvents()
        {
            this.StateChanged = null;
            this.StateChanging = null;
        }

        public virtual void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.OutgoingConnection += this.OnOutgoingCall;

            terminal.Online += (port, args) => { this.PortState = PortState.Free; };

            terminal.Offline += (port, args) => { this.PortState = PortState.Disabled; };
        }
    }
}
