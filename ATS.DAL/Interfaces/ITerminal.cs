using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models;
using ATS.DAL.Models.Requests;
using ATS.DAL.Models.Responds;
using System;

namespace ATS.DAL.Interfaces
{
    public interface ITerminal : IEntity, IDisposable
    {
        string PhoneNumber { get; set; }

        Port ProvidedPort { get; set; }

        event EventHandler<Request> OutgoingConnection;

        event EventHandler<Request> IncomingRequest;

        event EventHandler<Respond> IncomingRespond;

        event EventHandler Online;

        event EventHandler Offline;

        public event EventHandler Connecting;

        public event EventHandler Disconnecting;

        void Call(string targetPhoneNumber);

        void Drop();

        void Answer();

        void Disconect(IPort port);

        void IncomingRequestFrom(string source);

        void RegisterEventHandlersForPort(IPort port);
        void RegisterEventHandlersForContract(IContract contract);
    }
}