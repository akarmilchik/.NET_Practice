using ATS.App.Constants;
using ATS.App.Interfaces;
using ATS.App.Requests;
using System;

namespace ATS.App.Models
{
    public class Terminal : ITerminal
    {
        private Request ServerIncomingRequest { get; set; }
        protected bool IsOnline { get; private set; }
        public PhoneNumber PhoneNumber { get; set; }

        public Terminal(PhoneNumber number)
        {
            this.PhoneNumber = number;
        }

        public void Answer()
        {
            if (!IsOnline && ServerIncomingRequest != null)
            {
                OnIncomingRespond(this, new Responds.Respond() { Source = PhoneNumber, State = RespondState.Accept, Request = ServerIncomingRequest });
                OnOnline(this, null);
            }
        }

        public void Call(PhoneNumber targetPhoneNumber)
        {
            throw new System.NotImplementedException();
        }

        public void Connect(IPort port)
        {
            throw new System.NotImplementedException();
        }

        public void Disconect(IPort port)
        {
            throw new System.NotImplementedException();
        }

        public void Drop()
        {
            if (ServerIncomingRequest != null)
            {
                OnIncomingRespond(this, new Responds.Respond() { Source = PhoneNumber, State = RespondState.Drop, Request = ServerIncomingRequest });
                if (IsOnline)
                {
                    OnOffline(this, null);
                }
            }
        }

        protected virtual void OnOutgoingCall(object sender, PhoneNumber target)
        {
            if (OutgoingConnection != null)
            {
                ServerIncomingRequest = new Requests.OutgoingCallRequest() { Source = this.PhoneNumber, Target = target };
                OutgoingConnection(sender, ServerIncomingRequest);
            }
        }

        public event EventHandler<Requests.IncomingCallRequest> IncomingRequest;

        protected virtual void OnIncomingRequest(object sender, Requests.IncomingCallRequest request)
        {
            if (IncomingRequest != null)
            {
                IncomingRequest(sender, request);
            }
            ServerIncomingRequest = request;
        }

        public void IncomingRequestFrom(PhoneNumber source)
        {
            OnIncomingRequest(this, new Requests.IncomingCallRequest() { Source = source });
        }

        public event EventHandler<Responds.Respond> IncomingRespond;

        protected virtual void OnIncomingRespond(object sender, Responds.Respond respond)
        {
            if (this.IncomingRespond != null && ServerIncomingRequest != null)
            {
                this.IncomingRespond(sender, respond);
            }
        }

        public event EventHandler Online;

        public event EventHandler Offline;

        protected virtual void OnOnline(object sender, EventArgs args)
        {
            if (Online != null)
            {
                Online(sender, args);
            }
            IsOnline = true;
        }

        protected virtual void OnOffline(object sender, EventArgs args)
        {
            if (Offline != null)
            {
                Offline(sender, args);
                ServerIncomingRequest = null;
            }
            IsOnline = false;
        }


        public void Plug()
        {
            OnPlugging(this, null);
        }

        public void Unplug()
        {
            if (IsOnline)
            {
                Drop();
                OnUnPlugging(this, null);
            }
        }

        protected virtual void OnPlugging(object sender, EventArgs args)
        {
            if (Plugging != null)
            {
                Plugging(sender, args);
            }
        }

        protected virtual void OnUnPlugging(object sender, EventArgs args)
        {
            if (UnPlugging != null)
            {
                UnPlugging(sender, args);
            }
        }

        public event EventHandler Plugging;

        public event EventHandler UnPlugging;

        public void ClearEvents()
        {
            this.IncomingRequest = null;
            this.IncomingRespond = null;
            this.Online = null;
            this.Offline = null;
            this.OutgoingConnection = null;
            this.Plugging = null;
            this.UnPlugging = null;
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

    }
}
