using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr4
{
    public class ChatRoom
    {
        private readonly List<Contact> _contacts = new List<Contact>();
        private readonly List<Message> _messages = new List<Message>();

        public IReadOnlyList<Contact> Contacts => _contacts;
        public IReadOnlyList<Message> Messages => _messages;

        public void AddMessage(Message message)
        {
            if (!_contacts.Contains(message.Author))
            {
                _contacts.Add(message.Author);
            }
            _messages.Add(message);
        }

        public IEnumerable<Message> GetMessagesFromContact(Contact contact) =>
            _messages.Where(m => m.Author == contact);

        public IEnumerable<Contact> GetContacts() => _contacts;

        public override string ToString() =>
            $"Chat Room: {_messages.Count} messages.";

        public (string, int, string) getTuple()
        {
            var groupedMessages = _messages.GroupBy(m => m.Author);
            var shortestMessage = groupedMessages.Select(group =>
            {
                var shortest = group.OrderBy(m => m.Text.Length).FirstOrDefault();
                return (group.Key.Name, group.Count(), shortest?.Text ?? "");
            }).OrderBy(tuple => tuple.Item3.Length).FirstOrDefault();

            return shortestMessage;
        }

        public void printMessageForTuple((string, int, string) tuple)
        {
            switch (tuple)
            {
                case (string contactName, int messageCount, string shortestMessage) when messageCount > 0:
                    Console.WriteLine($"Contact: {contactName}");
                    Console.WriteLine($"Number of messages: {messageCount}");
                    Console.WriteLine($"Shortest message: {shortestMessage}");
                    break;
                case (_, _, _):
                    Console.WriteLine("No messages found in the chat room.");
                    break;
                default:
                    Console.WriteLine("Unexpected result.");
                    break;
            }
        }
    }
}
