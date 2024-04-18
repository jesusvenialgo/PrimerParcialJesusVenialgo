using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public interface IFactura
    {
        public string add(FacturaModel factura);
        bool delete(FacturaModel factura);
        public string update(FacturaModel factura);

        FacturaModel get(int id);
        IEnumerable<FacturaModel> list();
    }
}
