using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SPI_Logradouro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                            USE [ClientsTGS]
                            GO
                            SET ANSI_NULLS ON
                            GO
                            SET QUOTED_IDENTIFIER ON
                            GO
                            -- ===============================================================================================        
                            -- Create date: 18/06/2023    
                            -- Author: Herbert Costa    
                            -- Description: Procedure responsável por adicionar logradouro a um cliente        
                            -- exec [SPI_Logradouro] 1, 'Rua A, Casa B, bairro centro SP/SP'
                            -- ================================================================================================ 
                                                                                                                                                          
                            -- ================================================================================================   
                            CREATE PROCEDURE [dbo].[SPI_Logradouro]         
                             @ClienteId int,
                             @Logradouro varchar(150)
                            AS        
                            SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED        
        
                            BEGIN

	                            INSERT INTO Address(Logradouro, ClienteId, CreateDate)
	                            VALUES(@Logradouro, @ClienteId, GETDATE())

                            END
                            -- ===============================================================================================   
                            -- FIM DA EXECUÇÃO DA PROCEDURE SPI_Logradouro  
                            -- ===============================================================================================  
                      ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE SPI_Logradouro";

            migrationBuilder.Sql(sp);
        }
    }
}
