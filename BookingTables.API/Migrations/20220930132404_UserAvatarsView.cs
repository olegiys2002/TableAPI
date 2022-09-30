using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTablesAPI.Migrations
{
    public partial class UserAvatarsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("create view UserAvatars as select Users.Id , Avatars.Image from Users inner join Avatars on Users.AvatarId = Avatars.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop view UserAvatars");
        }
    }
}
