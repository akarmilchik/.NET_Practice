using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models;
using ATS.DAL.Models.Requests;
using ATS.DAL.Models.Responds;
using System;

namespace ATS.DAL.Interfaces
{
    public interface ITerminal : IDisposable
    {
        int Id { get; set; }

        string PhoneNumber { get; set; }

        Port ProvidedPort { get; set; }

        Request ServerIncomingRequest { get; set; }

        event EventHandler<Request> OutgoingConnectionEstablished;

        event EventHandler<Request> IncomingRequestReceived;

        event EventHandler<Respond> IncomingRespondEstablished;

        event EventHandler TurnedToOn;

        event EventHandler TurnedToOff;

        event EventHandler PortConnectionEstablished;

        event EventHandler PortConnectionInterrupted;

        void Call(string targetPhoneNumber);

        void DropIncomingRespond();

        void Answer();

        void DisconectFromPort(IPort port);

        void ReceiveIncomingRequest(string source);

        void RegisterEventHandlersForPort(IPort port);

        void SubscribeToContractEvents(IContract contract);

        void ConnectToPort(IPort port);
    }
}