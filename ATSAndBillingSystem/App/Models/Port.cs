using ATS.App.Constants;
using ATS.App.Interfaces;
using System;

namespace ATS.App.Models
{
    public abstract class Port : IPort
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

        public void ClearEvents()
        {
            this.StateChanged = null;
            this.StateChanging = null;
        }

        public abstract void RegisterEventHandlersForTerminal(ITerminal terminal);
    }
}
