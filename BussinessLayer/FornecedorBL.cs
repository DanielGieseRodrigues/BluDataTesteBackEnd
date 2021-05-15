using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BussinessLayer
{
    public class FornecedorBL
    {
        BluDataContext context = new BluDataContext();

        #region Crud
        public string CadastrarFornecedor(int empresaId, string nome, string cpfcnpj, String[] telefones, string rg, DateTime? datanascimento)
        {
            try
            {

                if (cpfcnpj.Length == 11)
                {
                    if (rg == null || datanascimento == null)
                        return "Necessário cadastrar o RG e a data de nascimento para pessoas físicas!";

                    else if (context.Empresas.Find(empresaId).UF == "PR" && DateTime.Now.AddYears(-18) < datanascimento)
                    {
                        return "Caso a empresa seja do Paraná, não é possível cadastrar um fornecedor pessoa física menor de idade;";
                    }
                }

                //Usando objeto de transação TRANSACTIONSCOPE para evitar
                //erros no meio da operação e gravar apenas telefones.
                using (TransactionScope ts = new TransactionScope())
                {
                    Fornecedor fornecedor = new Fornecedor();
                    fornecedor.EmpresaID = empresaId;
                    fornecedor.Nome = nome;
                    fornecedor.CpfCnpj = cpfcnpj;
                    fornecedor.DataDeCadastro = DateTime.Now;
                    fornecedor.RG = rg;
                    fornecedor.DataNascimento = datanascimento.Value;
                    context.Fornecedores.Add(fornecedor);
                    context.SaveChanges();

                    //Looping para adicionar todos os telefones da lista
                    List<Telefone> telefonesList = new List<Telefone>();
                    foreach (String telefone in telefones)
                    {
                        Telefone tel = new Telefone();
                        tel.NumeroTelefone = telefone;
                        tel.Fornecedor = fornecedor;
                        context.Telefones.Add(tel);
                        telefonesList.Add(tel);
                        context.SaveChanges();
                    }

                    ts.Complete();
                    return "Fornecedor inserido com sucesso!";
                }
            }
            catch (Exception ex)
            {
                return "Erro ao tentar cadastrar um fornecedor : " + ex.Message;
            };
        }
        public string ExcluirFornecedor(int id)
        {
            try
            {
                //Uso de transacao pra impedir problemas de telefones excluidos e fornecedores não
                using (TransactionScope ts = new TransactionScope())
                {
                    var telefonesDesteFornecedor = context.Telefones.Where(p => p.FornecedorId == id);
                    foreach (var telefone in telefonesDesteFornecedor)
                    {
                        context.Telefones.Remove(context.Telefones.Find(telefone.TelefoneID));
                    }

                    context.SaveChanges();

                    context.Fornecedores.Remove(context.Fornecedores.Find(id));
                    context.SaveChanges();
                    ts.Complete();
                    return "Fornecedor excluído com sucesso!";
                }

            }
            catch (Exception ex)
            {
                return "Erro ao tentar excluir um fornecedor : " + ex.Message;
            }
        }
        public List<Fornecedor> RetornarTodosOsFornecedores()
        {
            try
            {
                return context.Fornecedores.Include(x => x.Telefones).Include(p => p.Empresa).ToList();
            }
            catch (Exception)
            {
                return new List<Fornecedor>();
            }
        }
        public Fornecedor RetornarFornecedorPorId(int id)
        {
            try
            {
                return context.Fornecedores.ToList().Where(p => p.FornecedorID == id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Fornecedor();
            }
        }

        public List<Fornecedor> FiltrarFornecedores(string nome, string cpfCnpj, DateTime? dataCadastro)
        {
            try
            {
                List<Fornecedor> returnList = context.Fornecedores.Include(x => x.Telefones).Include(p => p.Empresa).ToList();
                if (nome != null)
                    returnList = returnList.Where(p => p.Nome == nome).ToList();
                if (cpfCnpj != null)
                    returnList = returnList.Where(p => p.CpfCnpj == cpfCnpj).ToList();
                
                if (dataCadastro != null)
                    returnList = returnList.Where(p => p.DataDeCadastro.Date == dataCadastro.Value.Date).ToList();

                return returnList;
            }
            catch (Exception)
            {
                return new List<Fornecedor>();
            }
        }
        #endregion
    }
}
