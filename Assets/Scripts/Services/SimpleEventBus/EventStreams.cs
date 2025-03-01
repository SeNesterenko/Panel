using SimpleEventBus.Interfaces;

namespace SimpleEventBus
{
    public static class EventStreams
    {
        public static IEventBus Game => _game ??= new EventBus();

        private static IEventBus _game;
    }
}