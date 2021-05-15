using BussinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BluDataTesteBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedorController : ControllerBase
    {
        FornecedorBL fornecedor = new FornecedorBL();

        [HttpGet]
        public List<Fornecedor> Get()
        {
            return fornecedor.RetornarTodosOsFornecedores();
        }

        [HttpGet("Add")]
        public string Add(int empresaId, string nome, string cpfcnpj, string telefones, string rg, string datanascimento)
        {
            DateTime? data = null;
            if (datanascimento != null)
                data = new DateTime(Convert.ToInt32(datanascimento.Substring(4, 4)), Convert.ToInt32(datanascimento.Substring(2, 2)), Convert.ToInt32(datanascimento.Substring(0, 2)));
            string[] telefonesArray = new string[1];
            if (telefones != null)
                telefonesArray = telefones.Split(",");

            return fornecedor.CadastrarFornecedor(empresaId, nome, cpfcnpj, telefonesArray, rg, data);
        }

        [HttpGet("delete")]
        public string Delete(int id)
        {
            return fornecedor.ExcluirFornecedor(id);
        }

        [HttpGet("{id}")]
        public Fornecedor FindById(int id)
        {
            return fornecedor.RetornarFornecedorPorId(id);
        }

        [HttpGet("filtrar")]
        public List<Fornecedor> Filtrar(String nome, string cpfCnpj,string datacadastro)
        {
            DateTime? data = null;
            if (datacadastro != null)
                data = new DateTime(Convert.ToInt32(datacadastro.Substring(4, 4)), Convert.ToInt32(datacadastro.Substring(2, 2)), Convert.ToInt32(datacadastro.Substring(0, 2)));
            
            return fornecedor.FiltrarFornecedores(nome,cpfCnpj,data);
        }
    }
}
