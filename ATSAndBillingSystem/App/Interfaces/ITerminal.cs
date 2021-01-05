using ATS.App.Models;
using ATS.App.Requests;
using ATS.App.Responds;
using System;

namespace ATS.App.Interfaces
{
    public interface ITerminal
    {
        PhoneNumber PhoneNumber { get; }

        event EventHandler<Request> OutgoingConnection;
        event EventHandler<IncomingRequest> IncomingRequest;
        event EventHandler<Respond> IncomingRespond;
        event EventHandler Online;
        event EventHandler Offline;

        void Call(PhoneNumber targetPhoneNumber);
        void Drop();
        void Answer();
        void Connect(IPort port);
        void Disconect(IPort port);
    }
}
