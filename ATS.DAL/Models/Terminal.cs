using ATS.DAL.Constants;
using ATS.DAL.Interfaces;
using ATS.DAL.Models.Requests;
using ATS.DAL.Models.Responds;
using System;

namespace ATS.DAL.Models
{
    public class Terminal : ITerminal
    {
        private readonly string _phoneNumber;
        private Request ServerIncomingRequest { get; set; }
        protected bool IsOnline { get; private set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public Port ProvidedPort { get; set; }

        public Terminal()
        {
        }

        public Terminal(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public void Answer()
        {
            if (!IsOnline && ServerIncomingRequest != null)
            {
                var respond =  new Respond()
                {
                    SourcePhoneNumber = _phoneNumber,
                    State = RequestRespondState.Accept,
                    Request = ServerIncomingRequest
                };

                IncomingRespond?.Invoke(this, respond);

                OnOnline(this, null);
            }
        }

        public void Call(string targetPhoneNumber)
        {
            if (!IsOnline)
            {
                ServerIncomingRequest = new OutgoingRequest()
                {
                    SourcePhoneNumber = _phoneNumber,
                    TargetPhoneNumber = targetPhoneNumber
                };

                OutgoingConnection?.Invoke(this, ServerIncomingRequest);

                OnOnline(this, null);
            }
        }

        public void Connect(IPort port)
        {
            OnConnect(this, null);
        }

        public void Disconect(IPort port)
        {
            if (IsOnline)
            {
                Drop();

                OnDisconnect(this, null);
            }
        }

        public void Drop()
        {
            if (ServerIncomingRequest != null)
            {
                var respond = new Respond()
                {
                    SourcePhoneNumber = _phoneNumber,
                    State = RequestRespondState.Drop,
                    Request = ServerIncomingRequest
                };

                IncomingRespond?.Invoke(this, respond);

                if (IsOnline)
                {
                    OnOffline(this, null);
                }
            }
        }

        public void IncomingRequestFrom(string source)
        {
            var request = new Request { SourcePhoneNumber = source };

            IncomingRequest?.Invoke(this, request);

            ServerIncomingRequest = request;
        }

        public virtual void RegisterEventHandlersForPort(IPort port)
        {
            port.StateChanged += (sender, state) =>
            {
                if (IsOnline && state == PortState.Enabled)
                {
                    OnOffline(sender, null);
                }
            };
        }

        public event EventHandler<Request> OutgoingConnection;
        public event EventHandler<Request> IncomingRequest;
        public event EventHandler<Respond> IncomingRespond;
        public event EventHandler Online;
        public event EventHandler Offline;
        public event EventHandler Connecting;
        public event EventHandler Disconnecting;

        protected virtual void OnOnline(object sender, EventArgs args)
        {
            Online?.Invoke(sender, args);
            
            IsOnline = true;
        }

        protected virtual void OnOffline(object sender, EventArgs args)
        {
            Offline?.Invoke(sender, args);

            ServerIncomingRequest = null;
            
            IsOnline = false;
        }

        protected virtual void OnConnect(object sender, EventArgs args) => Connecting?.Invoke(sender, args);

        protected virtual void OnDisconnect(object sender, EventArgs args) => Disconnecting?.Invoke(sender, args);

        public override string ToString()
        {
            return $"   Terminal\n      Number: {PhoneNumber}";
        }

        public void Dispose()
        {
            IncomingRequest = null;
            IncomingRespond = null;
            Online = null;
            Offline = null;
            OutgoingConnection = null;
            Connecting = null;
            Disconnecting = null;
        }
    }
}