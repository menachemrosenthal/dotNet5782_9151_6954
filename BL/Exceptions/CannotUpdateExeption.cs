using System;
using System.Runtime.Serialization;

namespace BL
{

    [Serializable]
    public class CannotUpdateExeption : Exception
    {
        public string ItemType { get; private set; }
        public int Id { get; private set; }

        public CannotUpdateExeption() : base() { }
        public CannotUpdateExeption(string message) : base(message) { }
        public CannotUpdateExeption(string message, Exception inner) : base(message, inner) { }
        protected CannotUpdateExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public CannotUpdateExeption(string itemType, int id) : base() { this.Id = id; this.ItemType = itemType; }
        public CannotUpdateExeption(string itemType, int id, string message) : base(message) { this.Id = id; this.ItemType = itemType; }
        public override string ToString()
        {
            return $"the {ItemType} id {Id} cannot be updated {Message}";
        }
    }
}


