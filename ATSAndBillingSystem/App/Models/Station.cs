using ATS.App.Interfaces;
using System;
using System.Collections.Generic;

namespace ATS.App.Models
{
    public abstract class Station : IStation
    {
        private ICollection<ICallDetails> _connectionCollection;
        private ICollection<ICallDetails> _callCollection;
        private ICollection<ITerminal> _terminalCollection;
        private ICollection<IPort> _portCollection;
        private IDictionary<PhoneNumber, IPort> _portMapping;

        public Station(ICollection<ITerminal> terminalCollection, ICollection<IPort> portCollection)
        {
            this._terminalCollection = terminalCollection;
            this._portCollection = portCollection;
            this._connectionCollection = new List<ICallDetails>();
            this._callCollection = new List<ICallDetails>();
            this._portMapping = new Dictionary<PhoneNumber, IPort>();
        }

        public event EventHandler<CallDetails> CallDetailsPrepared;

        protected virtual void OnCallInfoPrepared(object sender, CallDetails callDetails)
        {
            if (CallDetailsPrepared != null)
            {
                CallDetailsPrepared(sender, callDetails);
            }
        }
        /*
        public void AssignTerminalToAClient(ITerminal terminal, IUser client)
        {
            throw new System.NotImplementedException();
        }*/

        public void ProvidePortForTerminal(ITerminal terminal, IPort port)
        {
            throw new System.NotImplementedException();
        }

        public abstract void RegisterEventHandlersForPort(IPort port);

        public abstract void RegisterEventHandlersForTerminal(ITerminal terminal);
    }
}
