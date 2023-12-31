﻿using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ClientsContext _context;
        public IUnitOfWork UnitOfWork => _context;
        private readonly string _databaseConnection;

        public AddressRepository(ClientsContext context, IConfiguration configuration)
        {
            _context = context;
            _databaseConnection = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddAsync(Addres address)
        {
            using (var conn = new SqlConnection(_databaseConnection))
            {
                try
                {
                    conn.Open();

                    var sql = "SPI_Logradouro";

                    await conn.ExecuteAsync(sql, new { clienteId = address.ClienteId, logradouro = address.Logradouro }, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public async void Update(Addres address) 
        {
            using (var conn = new SqlConnection(_databaseConnection))
            {
                try
                {
                    conn.Open();

                    var sql = "SPU_Logradouro";

                    await conn.ExecuteAsync(sql, new { addressId = address.Id, logradouro = address.Logradouro }, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Delete(Addres address) =>
            _context.Address.Remove(address);

        public Addres GetById(int addressId) =>
            _context.Address
                .Include(x => x.Cliente).ThenInclude(u => u.Users)
                .FirstOrDefault(x => x.Id == addressId);

        public async Task<List<Addres>> GetByClienteId(int clienteId) 
        {
            IEnumerable<Addres> list;
            using (var conn = new SqlConnection(_databaseConnection))
            {
                try
                {
                    conn.Open();

                    var sql = "SPS_Logradouro";

                    list = await conn.QueryAsync<Addres>(sql, new { clienteId = clienteId }, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return list?.ToList();
            }
        }
    }
}
