#nullable enable
namespace UniT.Data
{
    using System;

    public interface IStorage
    {
        public bool CanStore(Type type);
    }
}