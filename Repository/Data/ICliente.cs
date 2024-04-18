using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public interface ICliente
    {
        public string add(ClienteModel cliente);
        public bool delete(ClienteModel cliente);
        public string update(ClienteModel cliente);

        ClienteModel get(int id);
        IEnumerable<ClienteModel> list(); 
    }
}
