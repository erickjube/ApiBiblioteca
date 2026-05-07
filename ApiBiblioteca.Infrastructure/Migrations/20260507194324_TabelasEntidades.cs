using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBiblioteca.Migrations
{
    /// <inheritdoc />
    public partial class TabelasEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimo_Cliente_ClienteId",
                table: "Emprestimo");

            migrationBuilder.DropForeignKey(
                name: "FK_Exemplar_Livro_LivroId",
                table: "Exemplar");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemEmprestimo_Exemplar_ExemplarId",
                table: "ItemEmprestimo");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemVenda_Exemplar_ExemplarId",
                table: "ItemVenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_AutorId",
                table: "Livro");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Categoria_CategoriaId",
                table: "Livro");

            migrationBuilder.DropForeignKey(
                name: "FK_Multa_Emprestimo_EmprestimoId",
                table: "Multa");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Cliente_ClienteId",
                table: "Venda");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Venda",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Multa",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EmprestimoId1",
                table: "Multa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemEmprestimoId1",
                table: "Multa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Livro",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Livro",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Livro",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "AutorId",
                table: "Livro",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Livro",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AutorId1",
                table: "Livro",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId1",
                table: "Livro",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ItemEmprestimo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Exemplar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LivroId",
                table: "Exemplar",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoDeBarras",
                table: "Exemplar",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Cliente",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Cliente",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Cliente",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Cliente",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categoria",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categoria",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Autor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nacionalidade",
                table: "Autor",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Autor",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Multa_EmprestimoId1",
                table: "Multa",
                column: "EmprestimoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Multa_ItemEmprestimoId",
                table: "Multa",
                column: "ItemEmprestimoId");

            migrationBuilder.CreateIndex(
                name: "IX_Multa_ItemEmprestimoId1",
                table: "Multa",
                column: "ItemEmprestimoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_AutorId1",
                table: "Livro",
                column: "AutorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_CategoriaId1",
                table: "Livro",
                column: "CategoriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Isbn",
                table: "Livro",
                column: "Isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exemplar_CodigoDeBarras",
                table: "Exemplar",
                column: "CodigoDeBarras",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Cpf",
                table: "Cliente",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Email",
                table: "Cliente",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimo_Cliente_ClienteId",
                table: "Emprestimo",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exemplar_Livro_LivroId",
                table: "Exemplar",
                column: "LivroId",
                principalTable: "Livro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEmprestimo_Exemplar_ExemplarId",
                table: "ItemEmprestimo",
                column: "ExemplarId",
                principalTable: "Exemplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVenda_Exemplar_ExemplarId",
                table: "ItemVenda",
                column: "ExemplarId",
                principalTable: "Exemplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_AutorId",
                table: "Livro",
                column: "AutorId",
                principalTable: "Autor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_AutorId1",
                table: "Livro",
                column: "AutorId1",
                principalTable: "Autor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Categoria_CategoriaId",
                table: "Livro",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Categoria_CategoriaId1",
                table: "Livro",
                column: "CategoriaId1",
                principalTable: "Categoria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Multa_Emprestimo_EmprestimoId",
                table: "Multa",
                column: "EmprestimoId",
                principalTable: "Emprestimo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Multa_Emprestimo_EmprestimoId1",
                table: "Multa",
                column: "EmprestimoId1",
                principalTable: "Emprestimo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Multa_ItemEmprestimo_ItemEmprestimoId",
                table: "Multa",
                column: "ItemEmprestimoId",
                principalTable: "ItemEmprestimo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Multa_ItemEmprestimo_ItemEmprestimoId1",
                table: "Multa",
                column: "ItemEmprestimoId1",
                principalTable: "ItemEmprestimo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Cliente_ClienteId",
                table: "Venda",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimo_Cliente_ClienteId",
                table: "Emprestimo");

            migrationBuilder.DropForeignKey(
                name: "FK_Exemplar_Livro_LivroId",
                table: "Exemplar");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemEmprestimo_Exemplar_ExemplarId",
                table: "ItemEmprestimo");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemVenda_Exemplar_ExemplarId",
                table: "ItemVenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_AutorId",
                table: "Livro");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_AutorId1",
                table: "Livro");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Categoria_CategoriaId",
                table: "Livro");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Categoria_CategoriaId1",
                table: "Livro");

            migrationBuilder.DropForeignKey(
                name: "FK_Multa_Emprestimo_EmprestimoId",
                table: "Multa");

            migrationBuilder.DropForeignKey(
                name: "FK_Multa_Emprestimo_EmprestimoId1",
                table: "Multa");

            migrationBuilder.DropForeignKey(
                name: "FK_Multa_ItemEmprestimo_ItemEmprestimoId",
                table: "Multa");

            migrationBuilder.DropForeignKey(
                name: "FK_Multa_ItemEmprestimo_ItemEmprestimoId1",
                table: "Multa");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Cliente_ClienteId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Multa_EmprestimoId1",
                table: "Multa");

            migrationBuilder.DropIndex(
                name: "IX_Multa_ItemEmprestimoId",
                table: "Multa");

            migrationBuilder.DropIndex(
                name: "IX_Multa_ItemEmprestimoId1",
                table: "Multa");

            migrationBuilder.DropIndex(
                name: "IX_Livro_AutorId1",
                table: "Livro");

            migrationBuilder.DropIndex(
                name: "IX_Livro_CategoriaId1",
                table: "Livro");

            migrationBuilder.DropIndex(
                name: "IX_Livro_Isbn",
                table: "Livro");

            migrationBuilder.DropIndex(
                name: "IX_Exemplar_CodigoDeBarras",
                table: "Exemplar");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_Cpf",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_Email",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "EmprestimoId1",
                table: "Multa");

            migrationBuilder.DropColumn(
                name: "ItemEmprestimoId1",
                table: "Multa");

            migrationBuilder.DropColumn(
                name: "AutorId1",
                table: "Livro");

            migrationBuilder.DropColumn(
                name: "CategoriaId1",
                table: "Livro");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Venda",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Multa",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Livro",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Livro",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<long>(
                name: "CategoriaId",
                table: "Livro",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "AutorId",
                table: "Livro",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Livro",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ItemEmprestimo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Exemplar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<long>(
                name: "LivroId",
                table: "Exemplar",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoDeBarras",
                table: "Exemplar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Categoria",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Autor",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nacionalidade",
                table: "Autor",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Autor",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimo_Cliente_ClienteId",
                table: "Emprestimo",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exemplar_Livro_LivroId",
                table: "Exemplar",
                column: "LivroId",
                principalTable: "Livro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEmprestimo_Exemplar_ExemplarId",
                table: "ItemEmprestimo",
                column: "ExemplarId",
                principalTable: "Exemplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVenda_Exemplar_ExemplarId",
                table: "ItemVenda",
                column: "ExemplarId",
                principalTable: "Exemplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_AutorId",
                table: "Livro",
                column: "AutorId",
                principalTable: "Autor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Categoria_CategoriaId",
                table: "Livro",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Multa_Emprestimo_EmprestimoId",
                table: "Multa",
                column: "EmprestimoId",
                principalTable: "Emprestimo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Cliente_ClienteId",
                table: "Venda",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
