namespace FI.AtividadeEntrevista.BLL.ViewObjects
{
    public class DefaultReturnBLLVO<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T Object { get; set; }
    }
}
