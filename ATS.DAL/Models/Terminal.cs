using ATS.DAL.Constants;
using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models.Requests;
using ATS.DAL.Models.Responds;
using System;

namespace ATS.DAL.Models
{
    public class Terminal : ITerminal
    {
        public Request ServerIncomingRequest { get; set; }
        protected bool IsOnline { get; private set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public Port ProvidedPort { get; set; }

        public Terminal()
        {
        }

        public void Answer()
        {
            if (!IsOnline && ServerIncomingRequest != null)
            {
                var respond =  new Respond()
                {
                    SourcePhoneNumber = PhoneNumber,
                    State = RequestRespondState.Accept,
                    Request = ServerIncomingRequest
                };

                IncomingRespondEstablished?.Invoke(this, respond);

            }
        }

        public void Call(string targetPhoneNumber)
        {
            if (!IsOnline)
            {
                ServerIncomingRequest = new OutgoingRequest()
                {
                    SourcePhoneNumber = PhoneNumber,
                    TargetPhoneNumber = targetPhoneNumber
                };

                OutgoingConnectionEstablished?.Invoke(this, ServerIncomingRequest);
            }
        }

        public void ConnectToPort(IPort port)
        {
            OnTurnedToOn(this, null);
        }

        public void DisconectFromPort(IPort port)
        {
            if (IsOnline)
            {
                DropIncomingRespond();

                OnPortConnectionInterrupted(this, null);
            }
        }

        public void DropIncomingRespond()
        {
            if (ServerIncomingRequest != null)
            {
                var respond = new Respond()
                {
                    SourcePhoneNumber = PhoneNumber,
                    State = RequestRespondState.Drop,
                    Request = ServerIncomingRequest
                };

                IncomingRespondEstablished?.Invoke(this, respond);

                if (IsOnline)
                {
                    OnTurnedToOff(this, null);
                }
            }
        }

        public void ReceiveIncomingRequest(string source)
        {
            var request = new Request { SourcePhoneNumber = source };

            IncomingRequestReceived?.Invoke(this, request);

            ServerIncomingRequest = request;
        }

        public virtual void RegisterEventHandlersForPort(IPort port)
        {
            port.StateChanged += (sender, state) =>
            {
                if (IsOnline && state == PortState.Enabled)
                {
                    OnTurnedToOff(sender, null);
                }
            };
        }

        public virtual void SubscribeToContractEvents(IContract contract)
        {
            contract.ContractConcluded += OnTurnedToOn;

            contract.ContractConcluded += OnPortConnectionEstablished;
        }

        public event EventHandler<Request> OutgoingConnectionEstablished;
        public event EventHandler<Request> IncomingRequestReceived;
        public event EventHandler<Respond> IncomingRespondEstablished;
        public event EventHandler TurnedToOn;
        public event EventHandler TurnedToOff;
        public event EventHandler PortConnectionEstablished;
        public event EventHandler PortConnectionInterrupted;

        protected virtual void OnTurnedToOn(object sender, ITerminal terminal)
        { 
            TurnedToOn?.Invoke(sender, null);
            
            IsOnline = true;
        }

        protected virtual void OnTurnedToOff(object sender, EventArgs args)
        {
            TurnedToOff?.Invoke(sender, args);

            ServerIncomingRequest = null;
            
            IsOnline = false;
        }

        protected virtual void OnPortConnectionEstablished(object sender, ITerminal terminal) => PortConnectionEstablished?.Invoke(sender, null);

        protected virtual void OnPortConnectionInterrupted(object sender, EventArgs args) => PortConnectionInterrupted?.Invoke(sender, args);

        public override string ToString()
        {
            return $"Terminal №{Id}. Number:{PhoneNumber}";
        }

        public void Dispose()
        {
            IncomingRequestReceived = null;
            IncomingRespondEstablished = null;
            TurnedToOn = null;
            TurnedToOff = null;
            OutgoingConnectionEstablished = null;
            PortConnectionEstablished = null;
            PortConnectionInterrupted = null;
        }
    }
}