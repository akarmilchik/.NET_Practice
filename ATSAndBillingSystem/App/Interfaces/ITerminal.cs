﻿using ATS.App.Requests;
using ATS.App.Responds;
using System;

namespace ATS.App.Interfaces
{
    public interface ITerminal : IClearEventsService
    {
        string PhoneNumber { get; }

        event EventHandler<Request> OutgoingConnection;

        event EventHandler<IncomingRequest> IncomingRequest;

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
