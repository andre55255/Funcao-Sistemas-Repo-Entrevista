using FI.AtividadeEntrevista.BLL.ViewObjects;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public DefaultReturnBLLVO<long> Incluir(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();

            var isExistCPF = cli.VerificarExistencia(cliente.CPF);

            if (isExistCPF)
                return new DefaultReturnBLLVO<long>
                {
                    IsSuccess = false,
                    Message = "Já existe um cliente cadastrado com o CPF informado",
                    Object = -1L
                };

            var result = cli.Incluir(cliente);

            return new DefaultReturnBLLVO<long>
            {
                IsSuccess = true,
                Message = "Cliente cadastrado com sucesso",
                Object = result
            };
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public DefaultReturnBLLVO<object> Alterar(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();

            var isExistCPF = cli.VerificarExistencia(cliente.CPF, cliente.Id);

            if (isExistCPF)
                return new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = "Já existe um cliente cadastrado com o CPF informado",
                };

            cli.Alterar(cliente);

            return new DefaultReturnBLLVO<object>
            {
                IsSuccess = true,
                Message = "Cliente inserido com sucesso",
            };
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(CPF);
        }
    }
}
