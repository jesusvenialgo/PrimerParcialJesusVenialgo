using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Services.Logica
{
    public class ClienteService
    {
        ClienteRepository clienteRepository;

        public ClienteService(string connectionString)
        {
            clienteRepository = new ClienteRepository(connectionString);
        }

        public string add(ClienteModel modelo)
        {
            return ValidarDatosCliente(modelo) ? clienteRepository.add(modelo) : throw new Exception("Se encontraron errores en la validación");
        }



        public string update(ClienteModel cliente)
        {
            if (ValidarDatosCliente(cliente))
            {
                return clienteRepository.update(cliente);
            }
            else
            {
                return "Se encontraron inconvenientes para actualizar";
            }
        }

        public IEnumerable<ClienteModel> listarclientes()
        {
            return clienteRepository.list();
        }


        public bool ValidarDatosCliente(ClienteModel cliente)
        {
            // Validar nombre
            if (string.IsNullOrEmpty(cliente.nombre) || cliente.nombre.Length < 3)
            {
                return false;
            }
            // Validar apellido
            if (string.IsNullOrEmpty(cliente.apellido) || cliente.apellido.Length < 3)
            {
                return false;
            }

            // Validar cédula
            if (string.IsNullOrEmpty(cliente.documento) || cliente.documento.Length < 3)
            {
                return false;
            }

            // Validar celular (opcional)
            if (!string.IsNullOrEmpty(cliente.celular))
            {
                if (!Int32.TryParse(cliente.celular, out _))
                {
                    return false;
                }
                else if (cliente.celular.Length != 10)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

