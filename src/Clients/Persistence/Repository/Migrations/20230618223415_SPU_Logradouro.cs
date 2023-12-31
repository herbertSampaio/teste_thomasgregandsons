﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SPU_Logradouro : Migration
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
                        -- Description: Procedure responsável por atualizar um logradouro        
                        -- exec [SPU_Logradouro] 5, 'Rua A, Casa B, bairro centro SP/SP'
                        -- ================================================================================================ 
                                                                                                                                                          
                        -- ================================================================================================   
                        CREATE PROCEDURE [dbo].[SPU_Logradouro]         
                            @AddressId int,
                            @Logradouro varchar(150)
                        AS        
                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED        
        
                        BEGIN

	                        UPDATE Address SET Logradouro = @Logradouro, UpdateDate = GETDATE() WHERE Id = @AddressId

                        END
                        -- ===============================================================================================   
                        -- FIM DA EXECUÇÃO DA PROCEDURE SPU_Logradouro  
                        -- ===============================================================================================   
                      ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE SPU_Logradouro";

            migrationBuilder.Sql(sp);
        }
    }
}
