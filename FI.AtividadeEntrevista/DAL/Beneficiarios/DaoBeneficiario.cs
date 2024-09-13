using FI.AtividadeEntrevista.BLL.CustomExceptions;
using FI.AtividadeEntrevista.BLL.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiario
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        internal long Incluir(DML.Beneficiario beneficiario)
        {
            try
            {
                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
                {
                    new System.Data.SqlClient.SqlParameter("@NOME", beneficiario.Nome),
                    new System.Data.SqlClient.SqlParameter("@CPF", beneficiario.CPF.RemoveFormattingCPF()),
                    new System.Data.SqlClient.SqlParameter("@IDCLIENTE", beneficiario.IdCliente)
                };

                DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
                long ret = 0;
                if (ds.Tables[0].Rows.Count > 0)
                    long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);

                return ret;
            }
            catch (Exception ex)
            {
                throw new DALException("Falha inesperada na inserção de beneficiário", ex);
            }
        }

        /// <summary>
        /// Consulta um cliente pelo Id
        /// </summary>
        internal DML.Beneficiario Consultar(long Id)
        {
            try
            {
                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
                {
                    new System.Data.SqlClient.SqlParameter("@ID", Id),
                    new System.Data.SqlClient.SqlParameter("@IDCLIENTE", 0)
                };

                DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
                List<DML.Beneficiario> cli = Converter(ds);

                return cli.FirstOrDefault();
            }
            catch (DALException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DALException($"Falha inesperada na consulta de beneficiário '{Id}'", ex);
            }
        }

        /// <summary>
        /// Método para verificar existência de beneficiário com o CPF e ID Cliente informado
        /// </summary>
        internal bool VerificarExistencia(string CPF, long idCliente, long idBeneficiario = 0)
        {
            try
            {
                var ds = base.Consultar("FI_SP_VerificaBeneficiario", new List<System.Data.SqlClient.SqlParameter>
                {
                    new System.Data.SqlClient.SqlParameter("@CPF", CPF.RemoveFormattingCPF()),
                    new System.Data.SqlClient.SqlParameter("@IDCLIENTE", idCliente)
                });

                if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows == null || ds.Tables[0].Rows.Count <= 0)
                    return false;

                int beneficiarioIdFound = 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row.ItemArray != null && row.ItemArray.Length > 0)
                    {
                        beneficiarioIdFound = int.Parse(row.ItemArray[0] + "");
                    }
                }

                if (beneficiarioIdFound <= 0)
                    return false;

                return beneficiarioIdFound != idBeneficiario;
            }
            catch (Exception ex)
            {
                throw new DALException($"Falha inesperada na consistência de beneficiário", ex);
            }
        }

        /// <summary>
        /// Método para listar de forma paginada dados de beneficiários por cliente id
        /// </summary>
        internal List<DML.Beneficiario> Pesquisa(long idCliente, int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            try
            {
                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
                {
                    new System.Data.SqlClient.SqlParameter("@idCliente", idCliente),
                    new System.Data.SqlClient.SqlParameter("@iniciarEm", iniciarEm),
                    new System.Data.SqlClient.SqlParameter("@quantidade", quantidade),
                    new System.Data.SqlClient.SqlParameter("@campoOrdenacao", campoOrdenacao),
                    new System.Data.SqlClient.SqlParameter("@crescente", crescente)
                };

                DataSet ds = base.Consultar("FI_SP_PesqBeneficiario", parametros);
                List<DML.Beneficiario> data = Converter(ds);

                int iQtd = 0;

                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

                qtd = iQtd;

                return data;
            }
            catch (DALException)
            {
                throw;
            }
            catch (Exception ex) 
            {
                throw new DALException($"Falha inesperada na listagem de beneficiários do cliente '{idCliente}'", ex);
            }
        }

        /// <summary>
        /// Lista todos os beneficiários sem paginação
        /// </summary>
        internal List<DML.Beneficiario> Listar(long idCliente)
        {
            try
            {
                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
                {
                    new System.Data.SqlClient.SqlParameter("ID", -1L),
                    new System.Data.SqlClient.SqlParameter("IDCLIENTE", idCliente)
                };

                DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
                List<DML.Beneficiario> cli = Converter(ds);

                return cli;
            }
            catch (DALException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DALException($"Falha inesperada na listagem de usuários", ex);
            }
        }

        /// <summary>
        /// Altera beneficiário
        /// </summary>
        internal void Alterar(DML.Beneficiario cliente)
        {
            try
            {
                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
                {
                    new System.Data.SqlClient.SqlParameter("@NOME", cliente.Nome),
                    new System.Data.SqlClient.SqlParameter("@CPF", cliente.CPF.RemoveFormattingCPF()),
                    new System.Data.SqlClient.SqlParameter("@ID", cliente.Id),
                    new System.Data.SqlClient.SqlParameter("@IDCLIENTE", cliente.IdCliente),
                };

                base.Executar("FI_SP_AltBeneficiario", parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha inesperada ao alterar dados de beneficiário", ex);
            }
        }

        /// <summary>
        /// Excluir beneficiário
        /// </summary>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("@ID", Id)
            };

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario dataAux = new DML.Beneficiario();
                    dataAux.Id = row.Field<long>("ID");
                    dataAux.CPF = row.Field<string>("CPF").AddFormattingCPF();
                    dataAux.Nome = row.Field<string>("NOME");
                    dataAux.IdCliente = row.Field<long>("IDCLIENTE");
                    lista.Add(dataAux);
                }
            }

            return lista;
        }
    }
}
