using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SPS_Logradouro : Migration
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
                        -- Description: Procedure responsável listar logradouros de um cliente        
                        -- exec [SPS_InserirLogradouro] 5
                        -- ================================================================================================ 
                                                                                                                                                          
                        -- ================================================================================================   
                        CREATE PROCEDURE [dbo].[SPS_Logradouro]         
                         @ClienteId int
                        AS        
                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED        
        
                        BEGIN

	                        SELECT * FROM Address WHERE ClienteId = @ClienteId

                        END
                        -- ===============================================================================================   
                        -- FIM DA EXECUÇÃO DA PROCEDURE SPS_Logradouro  
                        -- ===============================================================================================   
                      ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE SPS_Logradouro";

            migrationBuilder.Sql(sp);
        }
    }
}
