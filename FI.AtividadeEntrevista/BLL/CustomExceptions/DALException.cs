using System;

namespace FI.AtividadeEntrevista.BLL.CustomExceptions
{
    public class DALException : ApplicationException
    {
        public DALException(string message, Exception ex = null) : base(message, ex)
        {
        }
    }
}
