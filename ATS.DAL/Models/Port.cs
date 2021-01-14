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

        public Port()
        {
            StateChanged += OnStateChanged;
        }

        protected virtual void OnStateChanged(object sender, PortState state)
        {
            StateChanged?.Invoke(sender, state);
        }

        public void OnOutgoingCall(object sender, Request request)
        {
            if (request.GetType() == typeof(OutgoingRequest) && PortState == PortState.Enabled)
            {
                PortState = PortState.Calling;
            }
        }

        public void ClearEvents()
        {
            StateChanged = null;
        }

        public virtual void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.OutgoingConnection += this.OnOutgoingCall;

            terminal.Online += (port, args) => { PortState = PortState.Enabled; };

            terminal.Offline += (port, args) => { PortState = PortState.Disabled; };
        }
    }
}