using ATS.DAL.Constants;
using ATS.DAL.Interfaces;
using ATS.DAL.Models.Billing;
using ATS.DAL.Models.Requests;
using ATS.DAL.Models.Responds;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS.DAL.Models
{
    public class Station : IStation
    {
        private readonly ICollection<CallDetails> _connectionCollection;
        private readonly ICollection<CallDetails> _callCollection;
        private readonly ICollection<Terminal> _terminals;
        private readonly ICollection<Port> _ports;
        private readonly IDictionary<string, IPort> _portMapping;
        private readonly ICollection<SecondMinuteTariffPlan> _tariffPlans;
        private readonly ICollection<Contract> _contracts;

        public int Id { get; set; }
        public string Name { get; set; }

        public Station()
        {
        }

        public Station(ICollection<Terminal> terminals, ICollection<Port> ports, ICollection<Contract> contracts, ICollection<SecondMinuteTariffPlan> tariffPlans)
        {
            _terminals = terminals;
            _ports = ports;
            _connectionCollection = new List<CallDetails>();
            _callCollection = new List<CallDetails>();
            _portMapping = new Dictionary<string, IPort>();
            _contracts = contracts;
            _tariffPlans = tariffPlans;
        }

        public event EventHandler<CallDetails> CallDetailsPrepared;
        public event EventHandler<Contract> TerminateContract;


        public void OnIncomingCallRespond(object sender, Respond respond)
        {
            var registeredCallInfo = GetConnectionInfo(respond.SourcePhoneNumber);
            if (registeredCallInfo != null)
            {
                switch (respond.State)
                {
                    case RequestRespondState.Drop:
                        {
                            InterruptConnection(registeredCallInfo);
                            break;
                        }
                    case RequestRespondState.Accept:
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

        public void Add(Terminal terminal)
        {
            var freePort = _ports.Except(_portMapping.Values).FirstOrDefault();

            if (freePort != null)
            {
                _terminals.Add(terminal);

                MapTerminalToPort(terminal, freePort);

                this.RegisterEventHandlersForTerminal(terminal);
                this.RegisterEventHandlersForPort(freePort);
            }
        }

        public virtual void RegisterEventHandlersForPort(IPort port)
        {
            port.StateChanged += (sender, state) => { Console.WriteLine("Station detected the port changed its State to {0}", state); };
        }

        public virtual void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            terminal.OutgoingConnection += OnOutgoingRequest;
            terminal.IncomingRespond += OnIncomingCallRespond;
        }

        protected ITerminal GetTerminalByPhoneNumber(string number)
        {
            return _terminals.FirstOrDefault(x => x.PhoneNumber == number);
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

                if (targetPort.PortState == PortState.Enabled)
                {
                    _connectionCollection.Add(callDetails);

                    targetPort.PortState = PortState.Calling;

                    targetTerminal.IncomingRequestFrom(request.SourcePhoneNumber);
                }
                else
                {
                    CallDetailsPrepared?.Invoke(this, callDetails);
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

            terminal.Dispose();

            port.Dispose();
        }

        protected void SetPortStateWhenConnectionInterrupted(string source, string target)
        {
            var sourcePort = GetPortByPhoneNumber(source);

            if (sourcePort != null)
            {
                sourcePort.PortState = PortState.Enabled;
            }

            var targetPort = GetPortByPhoneNumber(target);

            if (targetPort != null)
            {
                targetPort.PortState = PortState.Enabled;
            }
        }

        protected void InterruptConnection(CallDetails callDetails)
        {
            _callCollection.Remove(callDetails);

            SetPortStateWhenConnectionInterrupted(callDetails.Source, callDetails.Target);

            CallDetailsPrepared?.Invoke(this, callDetails);
        }

        protected void InterruptActiveCall(CallDetails callDetails)
        {
            callDetails.DurationTime = DateTime.Now - callDetails.StartedTime;

            _callCollection.Remove(callDetails);

            SetPortStateWhenConnectionInterrupted(callDetails.Source, callDetails.Target);

            CallDetailsPrepared?.Invoke(this, callDetails);
        }

        protected void MakeCallActive(CallDetails callDetails)
        {
            _connectionCollection.Remove(callDetails);

            callDetails.StartedTime = DateTime.Now;

            _callCollection.Add(callDetails);
        }

        public void OnOutgoingRequest(object sender, Request request)
        {
            if (request.GetType() == typeof(OutgoingRequest))
            {
                RegisterOutgoingRequest(request as OutgoingRequest);
            }
        }


        public void onTerminateContract(object sender, Contract contract)
        {
            TerminateContract?.Invoke(sender, contract);
        }

        public override string ToString()
        {
            return $"Station: {Name}";
        }

        public void Dispose()
        {
            CallDetailsPrepared = null;
            TerminateContract = null;
        }
    }
}