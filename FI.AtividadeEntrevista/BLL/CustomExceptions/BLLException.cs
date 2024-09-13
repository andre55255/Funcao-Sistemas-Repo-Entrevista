using System;

namespace FI.AtividadeEntrevista.BLL.CustomExceptions
{
    public class BLLException : ApplicationException
    {
        public BLLException(string message, Exception ex = null) : base(message, ex)
        {
        }
    }
}
