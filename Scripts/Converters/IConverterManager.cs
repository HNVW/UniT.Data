#nullable enable
namespace UniT.Data
{
    using System;

    public interface IConverterManager
    {
        public IConverter GetConverter(Type type);

        public object? GetDefaultValue(Type type) => this.GetConverter(type).GetDefaultValue(type);

        public object ConvertFromString(Type type, string str) => this.GetConverter(type).ConvertFromString(type, str);

        public string ConvertToString(Type type, object obj) => this.GetConverter(type).ConvertToString(type, obj);

        public T? GetDefaultValue<T>() where T : notnull => (T?)this.GetDefaultValue(typeof(T));

        public T ConvertFromString<T>(string str) where T : notnull => (T)this.ConvertFromString(typeof(T), str);

        public string ConvertToString<T>(T obj) where T : notnull => this.ConvertToString(typeof(T), obj);
    }
}