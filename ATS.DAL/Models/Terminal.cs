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
            this.IncomingRequest += this.OnIncomingRequest;
            this.IncomingRespond += this.OnIncomingRespond;
            this.Online += (sender, args) => { Console.WriteLine("Terminal {0} turned to online mode", phoneNumber); };
            this.Offline += (sender, args) => { Console.WriteLine("Terminal {0} turned to offline mode", phoneNumber); };
            this.Connecting += (sender, args) => { Console.WriteLine("Terminal {0} turned to offline mode", phoneNumber); };
            this.Disconnecting += (sender, args) => { Console.WriteLine("Terminal {0} turned to offline mode", phoneNumber); };
        }

        public void Answer()
        {
            if (!IsOnline && ServerIncomingRequest != null)
            {
                OnIncomingRespond(this, new Respond()
                {
                    SourcePhoneNumber = _phoneNumber,
                    State = RequestRespondState.Accept,
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
                //OutgoingConnection.Invoke(this, targetPhoneNumber);
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
                    SourcePhoneNumber = _phoneNumber,
                    State = RequestRespondState.Drop,
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
            OnIncomingRequest(this, new Request { SourcePhoneNumber = source });
        }

        public void ClearEvents()
        {
            IncomingRequest = null;
            IncomingRespond = null;
            Online = null;
            Offline = null;
            OutgoingConnection = null;
            Connecting = null;
            Disconnecting = null;
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

        protected virtual void OnOutgoingCall(object sender, string target)
        {
            if (OutgoingConnection != null)
            {
                ServerIncomingRequest = new OutgoingRequest()
                {
                    SourcePhoneNumber = _phoneNumber,
                    TargetPhoneNumber = target
                };
            }

            OutgoingConnection?.Invoke(sender, ServerIncomingRequest);
            
        }

        public event EventHandler<Request> IncomingRequest;

        protected virtual void OnIncomingRequest(object sender, Request request)    //add request state incoming
        {
            
            IncomingRequest?.Invoke(sender, request);

            Console.WriteLine("{0} received request for incoming connection from {1}", _phoneNumber, request.SourcePhoneNumber);
            

            ServerIncomingRequest = request;
        }

        public event EventHandler<Respond> IncomingRespond;

        protected virtual void OnIncomingRespond(object sender, Respond respond)
        {
            if (IncomingRespond != null && ServerIncomingRequest != null)
            {
                IncomingRespond(sender, respond);
                Console.WriteLine("{0} create respond for incoming connection from {1}", PhoneNumber, respond.SourcePhoneNumber);
            }
        }

        public event EventHandler Online;

        protected virtual void OnOnline(object sender, EventArgs args)
        {
            Online?.Invoke(sender, args);
            
            IsOnline = true;
        }

        public event EventHandler Offline;

        protected virtual void OnOffline(object sender, EventArgs args)
        {
            Offline?.Invoke(sender, args);

            ServerIncomingRequest = null;
            
            IsOnline = false;
        }

        public event EventHandler Connecting;

        protected virtual void OnConnect(object sender, EventArgs args) => Connecting?.Invoke(sender, args);

        public event EventHandler Disconnecting;

        protected virtual void OnDisconnect(object sender, EventArgs args) => Disconnecting?.Invoke(sender, args);
    }
}