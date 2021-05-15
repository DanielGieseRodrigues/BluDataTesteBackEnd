using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BussinessLayer
{
    public class EmpresaBL
    {
        BluDataContext context = new BluDataContext();

        #region Crud
        public string CadastrarEmpresa(string cnpj,string nomeFantasia,string UF)
        {
            try
            {
                if (cnpj.Length != 14)
                    return "O Cnpj está com o número de digitos incorretos, verifique se você incluiu pontuação!";

                Empresa empresa = new Empresa();
                empresa.CNPJ = cnpj;
                empresa.NomeFantasia = nomeFantasia;
                empresa.UF = UF;
                context.Empresas.Add(empresa);
                context.SaveChanges();
                return "Empresa cadastrada com sucesso!";
            }
            catch (Exception ex)
            {
                return "Erro ao tentar cadastrar uma empresa : " + ex.Message;
            };
        }
        public string ExcluirEmpresa (int id)
        {
            try
            {
                context.Empresas.Remove(context.Empresas.Find(id));
                context.SaveChanges();
                return "Empresa excluída com sucesso!";
            }
            catch (Exception ex)
            {
                return "Erro ao tentar excluir uma empresa : " + ex.Message;
            }
        }
        public List<Empresa> RetornarTodasAsEmpresas()
        {
            try
            {
                return context.Empresas.ToList();
            }
            catch (Exception)
            {
                return new List<Empresa>();
            }
        }
        public Empresa RetornarEmpresaPorId(int id)
        {
            try
            {
                return context.Empresas.ToList().Where(p => p.EmpresaID == id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Empresa();
            }
        }
        #endregion
    }
}
