using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Services.Logica
{
    public class FacturaService
    {
        FacturaRepository facturaRepository;

        public FacturaService(string connectionString)
        {
            facturaRepository = new FacturaRepository(connectionString);
        }

        public string add(FacturaModel modelo)
        {
            return ValidarDatosFacturas(modelo) ? facturaRepository.add(modelo) : throw new Exception("Se encontraron errores en la validación");
        }



        public string update(FacturaModel factura)
        {
            if (ValidarActualizacionFactura(factura.id, factura))
            {
                if (ValidarDatosFacturas(factura))
                {
                    return facturaRepository.update(factura);
                }
                else
                {
                    return ("Se encontraron errores en la validación");
                }
            }
            else
            {
                return ("El cliente con el ID especificado no existe");
            }


            if (ValidarDatosFacturas(factura))
            {
                return facturaRepository.update(factura);
            }
            else
            {
                throw new Exception("Se encontraron errores en la validación");
            }
        }

        public string delete(int id)
        {
            if (ValidarEliminacionFactura(id))
            {
                return facturaRepository.delete(id);
            }
            else
            {
                return "Se encontraron errores en la validación.";
            }

        }

        public IEnumerable<FacturaModel> listarfacturas()
        {
            return facturaRepository.list();
        }


        public bool ValidarDatosFacturas(FacturaModel factura)
        {
            return true;
            // Validar Nro. Factura
            if (string.IsNullOrEmpty(factura.nro_factura) || !Regex.IsMatch(factura.nro_factura, @"^\d{3}-\d{3}-\d{6}$"))
            {
                return false;
            }

            // Validar Total, Total_iva5, Total_iva10, Total_iva (datos numéricos obligatorios)
            if (!decimal.TryParse(factura.total, out decimal total) || total <= 0 ||
                !decimal.TryParse(factura.total_iva5, out decimal totalIva5) || totalIva5 <= 0 ||
                !decimal.TryParse(factura.total_iva10, out decimal totalIva10) || totalIva10 <= 0 ||
                !decimal.TryParse(factura.total_iva, out decimal totalIva) || totalIva <= 0)
            {
                return false;
            }

            // Validar Total en letras (obligatorio, al menos 6 caracteres)
            if (string.IsNullOrEmpty(factura.total_letras) || factura.total_letras.Length < 6)
            {
                return false;
            }

            return true;
        }

        public bool ValidarActualizacionFactura(int id, FacturaModel factura)
        {
            // Verificar si existe el cliente con el ID especificado
            var facturaExistente = facturaRepository.get(id);
            if (facturaExistente == null)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        public bool ValidarEliminacionFactura(int id)
        {
            // Verificar si existe el cliente con el ID especificado
            var facturaExistente = facturaRepository.get(id);
            if (facturaExistente == null)
            {
                return false; //"La factura no existe, no se puede eliminar";
            }

            return true; //"La factura existe, se puede proceder con la eliminación";
        }
    }
}
