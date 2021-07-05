using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using System;

namespace EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreService(IConfiguration configuration)
        {
            _connection = EventStoreConnection.Create(new Uri(configuration.GetConnectionString("EventStoreConnection")));

            _connection.ConnectAsync().Wait();
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;
        }
    }
}