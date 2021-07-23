using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Memki.Migrator
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify connection string");
                Environment.Exit(1);
            }
            var connectionString = args[0];

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMigrator(connectionString);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var migrationRunner = serviceProvider.GetRequiredService<IMigrationRunner>();
            migrationRunner.MigrateUp();
        }
    }
}