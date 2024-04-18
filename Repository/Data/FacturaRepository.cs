using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;


namespace Repository.Data
{
    public class FacturaRepository
    {
        public IDbConnection conexionDB;
        public FacturaRepository(string _connectionString)
        {
            conexionDB = new DbConection(_connectionString).dbConnection();
        }
        public string add(FacturaModel factura)
        {
            try
            {
                conexionDB.Execute("Insert into public.factura (id, id_cliente, nro_factura, fecha_hora, total, " +
                    "total_iva5, total_iva10, total_iva, total_letras, sucursal) values (@id, @id_cliente, @nro_factura, @fecha_hora, @total," +
                    "@total_iva5, @total_iva10, @total_iva, @total_letras, @sucursal)", factura);
                return ("El Registro fue agregado correctamente");

            }
            catch (Exception ex)
            {
                throw ex;
                return "Error al intentar agregar el registro: " + ex.Message;
            }

        }
        public string update(FacturaModel factura)
        {
            try
            {
                conexionDB.Execute("Update public.factura set id_cliente = @id_cliente, nro_factura = @nro_factura, fecha_hora = @fecha_hora, total_iva5 = @total_iva5," +
                    $"total_iva10 = @total_iva10, total_iva = @total_iva, total_letras = @total_letras, sucursal = @sucursal where id = {factura.id}", factura);

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
                if (conexionDB.Execute("Delete from public.factura where id = @id", new { id }) > 0)
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

        public FacturaModel get(int id)
        {
            try
            {
                var query = "Select * from public.factura where id = @id";
                return conexionDB.QuerySingleOrDefault<FacturaModel>(query, new { id = id });
                //return conexionDB.QuerySingleOrDefault<ClienteModel>("SELECT * from public.cliente where id= @id", cliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public IEnumerable<FacturaModel> list()
        {
            try
            {
                return conexionDB.Query<FacturaModel>("Select * from factura");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
