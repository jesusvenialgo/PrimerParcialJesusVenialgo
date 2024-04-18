using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Repository.Data
{
    public class ClienteRepository
    {
        public IDbConnection conexionDB;
        public ClienteRepository(string _connectionString)
        {
            conexionDB = new DbConection(_connectionString).dbConnection();
        }
        public string add(ClienteModel cliente)
        {
            try
            {
                conexionDB.Execute("Insert into public.cliente (id, id_banco, nombre, apellido, documento, " +
                    "direccion, mail, celular, estado) values (@id, @id_banco, @nombre, @apellido, @documento," +
                    "@direccion, @mail, @celular, @estado)", cliente);
                return ("El Registro fue agregado correctamente");

            }
            catch (Exception ex)
            {
                throw ex;
                return "Error al intentar agregar el registro: " + ex.Message;
            }
            
        }
        public string update(ClienteModel cliente)
        {
            try
            {
                conexionDB.Execute("Update public.cliente set id_banco = @id_banco, nombre = @nombre, apellido = @apellido, " +
                    $"documento = @documento, direccion = @direccion, mail = @mail, celular = @celular, estado = @estado where id = {cliente.id}", cliente);
                
                   return ("El Registro fue actualizado correctamente");
            }
            catch (Exception ex)
            {
                throw ex;
                return "Error al intentar actualizar el registro: " + ex.Message;
            }
        }

        public bool delete(int id)
        {
            try
            {
                if (conexionDB.Execute("Delete from public.cliente where id = @id", new { id }) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClienteModel get(int id)
        {
            try
            {
                var query = "Select * from public.cliente where id = @id";
                return conexionDB.QuerySingleOrDefault<ClienteModel>(query, new { id= id });
                //return conexionDB.QuerySingleOrDefault<ClienteModel>("SELECT * from public.cliente where id= @id", cliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public IEnumerable<ClienteModel> list()
        {
            try
            {
                return conexionDB.Query<ClienteModel>("Select * from cliente");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}