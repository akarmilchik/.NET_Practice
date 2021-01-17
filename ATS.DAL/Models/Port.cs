using ATS.DAL.Constants;
using ATS.DAL.Interfaces;
using ATS.DAL.Models.Requests;
using System;

namespace ATS.DAL.Models
{
    public class Port : IPort
    {
        private PortState _portState;

        public int Id { get; set; }

        public Port()
        {
            
        }

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
                    _portState = value;

                    OnStateChanged(this, _portState);
                }
            }
        }

        public event EventHandler<PortState> StateChanged;

        public event EventHandler<PortState> StateChanging;

        protected virtual void OnStateChanged(object sender, PortState state) => StateChanged?.Invoke(sender, state);

        protected virtual void OnStateChanging(object sender, PortState newState) => StateChanging?.Invoke(sender, newState);


        public void OnOutgoingCall(object sender, Request request)
        {
            if (request.GetType() == typeof(OutgoingRequest) && PortState == PortState.Enabled)
            {
                PortState = PortState.Calling;
            }
        }

        public virtual void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.TurnedToOn += (port, args) => { PortState = PortState.Enabled; };

            terminal.TurnedToOff += (port, args) => { PortState = PortState.Disabled; };
        }

        public override string ToString()
        {
            return $"   Provided port\n      №: {Id}\n      State: {PortState}";
        }

        public void Dispose()
        {
            StateChanged = null;

            StateChanging = null;
        }
    }
}