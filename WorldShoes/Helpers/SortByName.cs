using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldShoes.Helpers
{
    public class SortByName : IComparer<Produto>
    {
        public int Compare(Produto x, Produto y)
        {
            return x.Valor.CompareTo(y.Valor);
        }
       
    }
}