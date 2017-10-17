using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldShoes.Helpers
{
    public class SenhaAdmin
    {
        private string senha;
        public string CriarNovaSenhaAdmin()
        {
            this.senha = GerarDiaDaSemana() + GerarNumeroDoMes() + GerarSomaDiaEHoraAtual();
            return this.senha;
        }

        private string GerarDiaDaSemana()
        {
            var dia = DateTime.Now.DayOfWeek.ToString();
            switch (dia)
            {
                case "Sunday":
                    return "dom";
                case "Monday":
                    return "seg";
                case "Tuesday":
                    return "ter";
                case "Wednesday":
                    return "qua";
                case "Thursday":
                    return "qui";
                case "Friday":
                    return "sex";
                case "Saturday":
                    return "sab";
            }
            return "";
        }

        private string GerarNumeroDoMes()
        {
            var mes = DateTime.Now.Month;
            return mes < 10 ? "0" + mes.ToString() : mes.ToString();
        }

        private string GerarSomaDiaEHoraAtual()
        {
            var soma = DateTime.Now.Day + DateTime.Now.Hour;
            return soma.ToString();
        }
    }
}