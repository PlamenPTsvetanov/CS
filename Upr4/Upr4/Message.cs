using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr4
{
    public class Message
    {
        public Contact Author { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Edited { get; private set; }

        public Message(Contact author, string text)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Text = text ?? throw new ArgumentNullException(nameof(text));
            CreationDate = DateTime.Now;
            Edited = false;
        }

        public void EditMessage(string newText)
        {
            Text = newText;
            Edited = true;
        }

        public override string ToString()
        {
            string editedIndicator = Edited ? "(Edited)" : "";
            return $"{CreationDate} - {Author.Name}: {Text} {editedIndicator}";
        }

        public void Deconstruct( out DateTime creationDate)
        {
            creationDate = CreationDate;
        }
    }
}
