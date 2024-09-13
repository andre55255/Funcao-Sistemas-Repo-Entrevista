using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.BLL.CustomExceptions;
using FI.AtividadeEntrevista.BLL.ViewObjects;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        [HttpGet]
        public ActionResult ListBenefiniciarioView(long clienteId)
        {
            return PartialView(new BeneficiariosListVO
            {
                ClienteId = clienteId
            });
        }

        [HttpGet]
        public JsonResult ListBeneficiarioGet(long clienteId)
        {
            try
            {
                var beneficiarioBll = new BoBeneficiario();

                var listResult = beneficiarioBll.Listar(clienteId);

                if (!listResult.IsSuccess)
                    Response.StatusCode = 400;

                return Json(listResult, JsonRequestBehavior.AllowGet);
            }
            catch (BLLException ex)
            {
                Response.StatusCode = 400;
                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = $"Falha inesperada no fluxo de listagem de beneficiários ({ex.Message})"
                });
            }
        }

        [HttpPost]
        public JsonResult DeleteBeneficiario(long beneficiarioId)
        {
            try
            {
                var beneficiarioBll = new BoBeneficiario();

                var result = beneficiarioBll.Excluir(beneficiarioId);

                if (!result.IsSuccess)
                    Response.StatusCode = 400;

                return Json(result);
            }
            catch (BLLException ex)
            {
                Response.StatusCode = 400;

                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;

                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = $"Falha inesperada ao realizar fluxo de deleção de beneficiário ({ex.Message})"
                });
            }
        }

        [HttpPost]
        public JsonResult UpdateBeneficiarioPost(BeneficiarioModel model)
        {
            try
            {
                BoBeneficiario bo = new BoBeneficiario();

                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(new DefaultReturnBLLVO<object>
                    {
                        IsSuccess = false,
                        Message = string.Join(Environment.NewLine, erros)
                    });
                }

                var result = bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    CPF = model.CPF,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                });

                if (!result.IsSuccess)
                    Response.StatusCode = 400;

                return Json(result);
            }
            catch (BLLException ex)
            {
                Response.StatusCode = 400;

                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;

                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = $"Falha inesperada ao realizar fluxo de edição de beneficiário ({ex.Message})"
                });
            }
        }

        [HttpGet]
        public ActionResult UpdateBeneficiarioGet(long beneficiarioId)
        {
            try
            {
                var beneficiarioBll = new BoBeneficiario();

                var listResult = beneficiarioBll.Consultar(beneficiarioId);

                if (!listResult.IsSuccess)
                    Response.StatusCode = 400;

                return Json(listResult, JsonRequestBehavior.AllowGet);
            }
            catch (BLLException ex)
            {
                Response.StatusCode = 400;

                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;

                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = $"Falha inesperada ao realizar fluxo de busca de dados de beneficiário para edição ({ex.Message})"
                });
            }
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            try
            {
                BoBeneficiario bo = new BoBeneficiario();

                if (!this.ModelState.IsValid)
                {
                    string erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).FirstOrDefault();

                    Response.StatusCode = 400;
                    return Json(new DefaultReturnBLLVO<long>
                    {
                        IsSuccess = false,
                        Message = erros
                    });
                }

                var result = bo.Incluir(new Beneficiario()
                {
                    Id = model.Id,
                    CPF = model.CPF,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                });

                if (result.IsSuccess)
                {
                    model.Id = result.Object;
                    return Json(result);
                }

                Response.StatusCode = 400;
                return Json(result);
            }
            catch (BLLException ex)
            {
                Response.StatusCode = 400;

                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;

                return Json(new DefaultReturnBLLVO<object>
                {
                    IsSuccess = false,
                    Message = $"Falha inesperada ao realizar fluxo de inclusão de beneficiário ({ex.Message})"
                });
            }
        }
    }
}