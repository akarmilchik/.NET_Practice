using ATS.App.Constants;
using ATS.App.Interfaces;
using ATS.App.Requests;
using ATS.App.Responds;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS.App.Models
{
    public abstract class Station : IStation
    {
        private readonly ICollection<CallDetails> _connectionCollection;
        private readonly ICollection<CallDetails> _callCollection;
        private readonly ICollection<ITerminal> _terminalCollection;
        private readonly ICollection<IPort> _portCollection;
        private readonly IDictionary<string, IPort> _portMapping;

        public Station(ICollection<ITerminal> terminalCollection, ICollection<IPort> portCollection)
        {
            this._terminalCollection = terminalCollection;
            this._portCollection = portCollection;
            this._connectionCollection = new List<CallDetails>();
            this._callCollection = new List<CallDetails>();
            this._portMapping = new Dictionary<string, IPort>();
        }

        public void OnIncomingCallRespond(object sender, Respond respond)
        {
            var registeredCallInfo = GetConnectionInfo(respond.SourcePhoneNumber);
            if (registeredCallInfo != null)
            {
                switch (respond.State)
                {
                    case RespondState.Drop:
                        {
                            InterruptConnection(registeredCallInfo);
                            break;
                        }
                    case RespondState.Accept:
                        {
                            MakeCallActive(registeredCallInfo);
                            break;
                        }
                }
            }
            else
            {
                CallDetails currentCallInfo = GetCallDetails(respond.SourcePhoneNumber);

                if (currentCallInfo != null)
                {
                    this.InterruptActiveCall(currentCallInfo);
                }
            }
        }

        public void Add(ITerminal terminal)
        {
            var freePort = _portCollection.Except(_portMapping.Values).FirstOrDefault();
            if (freePort != null)
            {
                _terminalCollection.Add(terminal);

                MapTerminalToPort(terminal, freePort);

                this.RegisterEventHandlersForTerminal(terminal);
                this.RegisterEventHandlersForPort(freePort);
            }
        }

        public event EventHandler<CallDetails> CallDetailsPrepared;

        public void ClearEvents()
        {
            this.CallDetailsPrepared = null;
        }

        public abstract void RegisterEventHandlersForPort(IPort port);

        public abstract void RegisterEventHandlersForTerminal(ITerminal terminal);

        protected virtual void OnCallDetailsPrepared(object sender, CallDetails callDetails)
        {
            if (CallDetailsPrepared != null)
            {
                CallDetailsPrepared(sender, callDetails);
            }
        }

        protected ITerminal GetTerminalByPhoneNumber(string number)
        {
            return _terminalCollection.FirstOrDefault(x => x.PhoneNumber == number);
        }

        protected IPort GetPortByPhoneNumber(string number)
        {
            return _portMapping[number];
        }

        protected void RegisterOutgoingRequest(OutgoingRequest request)
        {
            if ((request.SourcePhoneNumber != default && request.TargetPhoneNumber != default) &&
                (GetCallDetails(request.SourcePhoneNumber) == null && GetConnectionInfo(request.SourcePhoneNumber) == null))
            {
                var callDetails = new CallDetails()
                {
                    Source = request.SourcePhoneNumber,
                    Target = request.TargetPhoneNumber,
                    StartedTime = DateTime.Now
                };

                ITerminal targetTerminal = GetTerminalByPhoneNumber(request.TargetPhoneNumber);

                IPort targetPort = GetPortByPhoneNumber(request.TargetPhoneNumber);

                if (targetPort.PortState == PortState.Free)
                {
                    _connectionCollection.Add(callDetails);

                    targetPort.PortState = PortState.Calling;

                    targetTerminal.IncomingRequestFrom(request.SourcePhoneNumber);
                }
                else
                {
                    OnCallDetailsPrepared(this, callDetails);
                }
            }
        }

        protected CallDetails GetConnectionInfo(string actor)
        {
            return _connectionCollection.FirstOrDefault(x => (x.Source == actor || x.Target == actor));
        }

        protected CallDetails GetCallDetails(string actor)
        {
            return _callCollection.FirstOrDefault(x => (x.Source == actor || x.Target == actor));
        }

        protected void MapTerminalToPort(ITerminal terminal, IPort port)
        {
            _portMapping.Add(terminal.PhoneNumber, port);

            port.RegisterEventHandlersForTerminal(terminal);

            terminal.RegisterEventHandlersForPort(port);
        }

        protected void UnMapTerminalFromPort(ITerminal terminal, IPort port)
        {
            _portMapping.Remove(terminal.PhoneNumber);

            terminal.ClearEvents();

            port.ClearEvents();
        }
        protected void SetPortStateWhenConnectionInterrupted(string source, string target)
        {
            var sourcePort = GetPortByPhoneNumber(source);

            if (sourcePort != null)
            {
                sourcePort.PortState = PortState.Free;
            }

            var targetPort = GetPortByPhoneNumber(target);

            if (targetPort != null)
            {
                targetPort.PortState = PortState.Free;
            }
        }

        protected void InterruptConnection(CallDetails callDetails)
        {
            _callCollection.Remove(callDetails);

            SetPortStateWhenConnectionInterrupted(callDetails.Source, callDetails.Target);

            OnCallDetailsPrepared(this, callDetails);
        }

        protected void InterruptActiveCall(CallDetails callDetails)
        {
            callDetails.DurationTime = DateTime.Now - callDetails.StartedTime;

            _callCollection.Remove(callDetails);

            SetPortStateWhenConnectionInterrupted(callDetails.Source, callDetails.Target);

            OnCallDetailsPrepared(this, callDetails);
        }

        protected void MakeCallActive(CallDetails callDetails)
        {
            _connectionCollection.Remove(callDetails);

            callDetails.StartedTime = DateTime.Now;

            _callCollection.Add(callDetails);
        }
    }
}
