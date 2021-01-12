using ATS.DAL.Interfaces.Services;
using ATS.DAL.Models.Requests;
using ATS.DAL.Models.Responds;
using System;

namespace ATS.DAL.Interfaces
{
    public interface ITerminal : IClearEventsService, IEntity
    {
        string PhoneNumber { get; set; }

        event EventHandler<Request> OutgoingConnection;

        event EventHandler<Request> IncomingRequest;

        event EventHandler<Respond> IncomingRespond;

        event EventHandler Online;

        event EventHandler Offline;

        void Call(string targetPhoneNumber);

        void Drop();

        void Answer();

        void Connect(IPort port);

        void Disconect(IPort port);

        void IncomingRequestFrom(string source);

        void RegisterEventHandlersForPort(IPort port);
    }
}