using FluentMigrator;

namespace Pushinator.Web.Migrations
{
    [Migration(2021_07_23_16_56)]
    public class _2021_07_23_16_56_AddUsersTable: AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsCustom("binary(16)").PrimaryKey().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Email").AsString().Nullable().Unique()
                .WithColumn("PasswordHash").AsString().Nullable();
        }
    }
}