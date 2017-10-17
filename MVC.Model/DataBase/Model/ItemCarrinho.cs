using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Model
{
    public class ItemCarrinho
    {
        public virtual Produto Produto { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual Decimal Total { get; set; }

    }
}
