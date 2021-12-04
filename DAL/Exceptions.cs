using System;
using System.Runtime.Serialization;

namespace IDAL
{
    [Serializable]
    public class AddExistException : Exception
    {
        public string ItemType { get; private set; }
        public int Id { get; private set; }
        public AddExistException() : base() { }
        public AddExistException(string message) : base(message) { }
        public AddExistException(string message, Exception inner) : base(message, inner) { }
        protected AddExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public AddExistException(string itemType, int id) : base() { this.Id = id; this.ItemType = itemType; }
        public override string ToString()
        {
            return $"the {ItemType} id {Id} is alredy exist";
        }
    }

    [Serializable]
    public class ItemNotFoundException : Exception
    {
        public string ItemType { get; private set; }
        public int Id { get; private set; }

        public ItemNotFoundException() : base() { }
        public ItemNotFoundException(string message) : base(message) { }
        public ItemNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public ItemNotFoundException(string itemType, int id) : base() { this.Id = id; this.ItemType = itemType; }
        public ItemNotFoundException(string itemType, int id, string message) : base(message) { this.Id = id; this.ItemType = itemType; }
        public override string ToString()
        {
            return $"the {ItemType} id {Id} is not found {Message}";
        }
    }

}
