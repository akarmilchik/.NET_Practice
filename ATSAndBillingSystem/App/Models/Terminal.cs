using ATS.App.Constants;
using ATS.App.Interfaces;
using ATS.App.Requests;
using ATS.App.Responds;
using System;

namespace ATS.App.Models
{
    public class Terminal : ITerminal
    {
        private Request ServerIncomingRequest { get; set; }
        protected bool IsOnline { get; private set; }
        public string PhoneNumber { get; private set; }

        public Terminal(string number)
        {
            PhoneNumber = number;
        }

        public void Answer()
        {
            if (!IsOnline && ServerIncomingRequest != null)
            {
                OnIncomingRespond(this, new Respond() 
                { 
                    SourcePhoneNumber = PhoneNumber, 
                    State = RespondState.Accept, 
                    Request = ServerIncomingRequest 
                });

                OnOnline(this, null);
            }
        }

        public void Call(string targetPhoneNumber)
        {
            if (!IsOnline)
            {
                OnOutgoingCall(this, targetPhoneNumber);
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
                OnIncomingRespond(this, new Respond() 
                { 
                    SourcePhoneNumber = PhoneNumber, 
                    State = RespondState.Drop, 
                    Request = ServerIncomingRequest 
                });
                
                if (IsOnline)
                {
                    OnOffline(this, null);
                }
            }
        }

        public void IncomingRequestFrom(string source)
        {
            OnIncomingRequest(this, new IncomingRequest() { SourcePhoneNumber = source });
        }

        public void ClearEvents()
        {
            this.IncomingRequest = null;
            this.IncomingRespond = null;
            this.Online = null;
            this.Offline = null;
            this.OutgoingConnection = null;
            this.Connecting = null;
            this.Disconnecting = null;
        }

        public virtual void RegisterEventHandlersForPort(IPort port)
        {
            port.StateChanged += (sender, state) =>
            {
                if (IsOnline && state == PortState.Free)
                {
                    this.OnOffline(sender, null);
                }
            };
        }

        public event EventHandler<Request> OutgoingConnection;

        protected virtual void OnOutgoingCall(object sender, string target)
        {
            if (OutgoingConnection != null)
            {
                ServerIncomingRequest = new OutgoingRequest() 
                { 
                    SourcePhoneNumber = this.PhoneNumber, 
                    TargetPhoneNumber = target 
                };

                OutgoingConnection(sender, ServerIncomingRequest);
            }
        }

        public event EventHandler<IncomingRequest> IncomingRequest;

        protected virtual void OnIncomingRequest(object sender, IncomingRequest request)
        {
            if (IncomingRequest != null)
            {
                IncomingRequest(sender, request);
            }

            ServerIncomingRequest = request;
        }

        public event EventHandler<Respond> IncomingRespond;

        protected virtual void OnIncomingRespond(object sender, Respond respond)
        {
            if (this.IncomingRespond != null && ServerIncomingRequest != null)
            {
                this.IncomingRespond(sender, respond);
            }
        }

        public event EventHandler Online;

        protected virtual void OnOnline(object sender, EventArgs args)
        {
            if (Online != null)
            {
                Online(sender, args);
            }

            IsOnline = true;
        }

        public event EventHandler Offline;

        protected virtual void OnOffline(object sender, EventArgs args)
        {
            if (Offline != null)
            {
                Offline(sender, args);

                ServerIncomingRequest = null;
            }

            IsOnline = false;
        }

        public event EventHandler Connecting;

        protected virtual void OnConnect(object sender, EventArgs args) => Connecting?.Invoke(sender, args);            

        public event EventHandler Disconnecting;

        protected virtual void OnDisconnect(object sender, EventArgs args) => Disconnecting?.Invoke(sender, args);
    }
}
