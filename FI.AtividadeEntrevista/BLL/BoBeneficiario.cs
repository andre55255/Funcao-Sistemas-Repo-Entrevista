using FI.AtividadeEntrevista.BLL.CustomExceptions;
using FI.AtividadeEntrevista.BLL.ViewObjects;
using System;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        public DefaultReturnBLLVO<long> Incluir(DML.Beneficiario beneficiario)
        {
            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();

                var isExistCPF = dao.VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente);

                if (isExistCPF)
                    return new DefaultReturnBLLVO<long>
                    {
                        IsSuccess = false,
                        Message = "Já existe um beneficiário cadastrado com o CPF informado",
                        Object = -1L
                    };

                var result = dao.Incluir(beneficiario);

                return new DefaultReturnBLLVO<long>
                {
                    IsSuccess = true,
                    Message = "Beneficiário cadastrado com sucesso",
                    Object = result
                };
            }
            catch (DALException ex)
            {
                throw new BLLException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BLLException("Falha inesperada no fluxo de inclusão de beneficiário", ex);
            }
        }

        /// <summary>
        /// Altera um beneficiário por id
        /// </summary>
        public DefaultReturnBLLVO<object> Alterar(DML.Beneficiario beneficiario)
        {
            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();

                var isExistCPF = dao.VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente, beneficiario.Id);

                if (isExistCPF)
                    return new DefaultReturnBLLVO<object>
                    {
                        IsSuccess = false,
                        Message = "Já existe um beneficiário cadastrado com o CPF informado",
                    };

                dao.Alterar(beneficiario);

                return new DefaultReturnBLLVO<object>
                {
                    IsSuccess = true,
                    Message = "Beneficiário inserido com sucesso",
                };
            }
            catch (DALException ex)
            {
                throw new BLLException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BLLException("Falha inesperada no fluxo de edição de beneficiário", ex);
            }
        }

        /// <summary>
        /// Consulta o beneficiário pelo id
        /// </summary>
        public DefaultReturnBLLVO<DML.Beneficiario> Consultar(long id)
        {
            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
                var data = dao.Consultar(id);

                if (data == null)
                    return new DefaultReturnBLLVO<DML.Beneficiario>
                    {
                        IsSuccess = false,
                        Message = $"Não foi encontrado um beneficiário com o id '{id}'"
                    };

                return new DefaultReturnBLLVO<DML.Beneficiario>
                {
                    IsSuccess = true,
                    Object = data,
                    Message = "Beneficiário listado com sucesso"
                };
            }
            catch (DALException ex)
            {
                throw new BLLException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BLLException($"Falha inesperada no fluxo de listagem de beneficiário '{id}'", ex);
            }
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        public DefaultReturnBLLVO<object> Excluir(long id)
        {
            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
                dao.Excluir(id);

                return new DefaultReturnBLLVO<object>
                {
                    IsSuccess = true,
                    Message = "Exclusão efetuada com sucesso"
                };
            }
            catch (DALException ex)
            {
                throw new BLLException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BLLException($"Falha inesperada no fluxo de deleção de beneficiário '{id}'", ex);
            }
        }

        /// <summary>
        /// Lista todos os beneficiários de um cliente id
        /// </summary>
        public DefaultReturnBLLVO<List<DML.Beneficiario>> Listar(long clienteId)
        {
            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
                var data = dao.Listar(clienteId);

                return new DefaultReturnBLLVO<List<DML.Beneficiario>>
                {
                    IsSuccess = true,
                    Message = "Beneficiários listados com sucesso",
                    Object = data
                };
            }
            catch (DALException ex)
            {
                throw new BLLException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BLLException($"Falha inesperada no fluxo de listagem completa de beneficiários '{clienteId}'", ex);
            }
        }

        /// <summary>
        /// Lista os beneficiários de um cliente id de forma paginada
        /// </summary>
        public DefaultReturnBLLVO<List<DML.Beneficiario>> Pesquisa(long clienteId, int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
                var data = dao.Pesquisa(clienteId, iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);

                return new DefaultReturnBLLVO<List<DML.Beneficiario>>
                {
                    IsSuccess = true,
                    Message = "Beneficiários listados com sucesso",
                    Object = data
                };
            }
            catch (DALException ex)
            {
                throw new BLLException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BLLException($"Falha inesperada no fluxo de listagem paginada de beneficiários '{clienteId}'", ex);
            }
        }

        /// <summary>
        /// Verifica existência de um benficiário com um cpf para um cliente id
        /// </summary>
        public DefaultReturnBLLVO<bool> VerificarExistencia(string cpf, long idCliente)
        {
            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
                var data = dao.VerificarExistencia(cpf, idCliente);

                return new DefaultReturnBLLVO<bool>
                {
                    IsSuccess = true,
                    Message = "Beneficiários consistido com sucesso",
                    Object = data
                };
            }
            catch (DALException ex)
            {
                throw new BLLException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BLLException($"Falha inesperada no fluxo de consistência de beneficiário", ex);
            }
        }
    }
}
