using Dodo.Primitives;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Memki.Model.Auth;

namespace Memki.Model
{
    public class Context: DataConnection
    {
        private static MappingSchema? _mappingSchema;

        public Context(string connectionString) : base(ProviderName.MySql, connectionString)
        {
            _mappingSchema ??= InitContextMappings(MappingSchema);
        }

        public ITable<User> Users => GetTable<User>();

        private static MappingSchema InitContextMappings(MappingSchema ms)
        {
            ms.GetFluentMappingBuilder()
                .Entity<User>()
                .HasTableName("Users")
                .Property(x => x.Id)
                .HasConversion(x => x.ToByteArray(), x => new Uuid(x))
                .HasDbType("binary(16)")
                .IsPrimaryKey()
                .Property(x => x.Email).IsNullable()
                .Property(x => x.PasswordHash).IsNullable()
                .Property(x => x.Name);

            return ms;
        }
    }
}