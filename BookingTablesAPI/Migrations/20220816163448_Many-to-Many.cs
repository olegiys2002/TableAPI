using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTablesAPI.Migrations
{
    public partial class ManytoMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

           migrationBuilder.CreateTable(
           name: "OrderTable",
           columns: table => new
           {
               OrdersId = table.Column<int>(type: "int", nullable: false),
               TableId = table.Column<int>(type: "int", nullable: false)
           },
           constraints: table =>
           {
               table.PrimaryKey("PK_OrderTable", x => new { x.OrdersId, x.TableId });
               table.ForeignKey(
                   name: "FK_OrderTable_Orders_OrdersId",
                   column: x => x.OrdersId,
                   principalTable: "Orders",
                   principalColumn: "Id",
                   onDelete: ReferentialAction.Cascade);
               table.ForeignKey(
                   name: "FK_OrderTable_Tables_TableId",
                   column: x => x.TableId,
                   principalTable: "Tables",
                   principalColumn: "Id",
                   onDelete: ReferentialAction.Cascade);
           });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_TableId",
                table: "OrderTable",
                column: "TableId");

            migrationBuilder.Sql("insert into OrderTable (OrdersId, TableId) select Orders.Id , Tables.Id from Orders inner join Tables on Orders.Id = Tables.OrderId ");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Orders_OrderId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_OrderId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Tables");

       
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTable");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Tables",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_OrderId",
                table: "Tables",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Orders_OrderId",
                table: "Tables",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
