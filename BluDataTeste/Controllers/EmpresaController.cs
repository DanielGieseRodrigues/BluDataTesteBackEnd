using BussinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BluDataTesteBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : ControllerBase
    {
        EmpresaBL empresa = new EmpresaBL();

        [HttpGet]
        public List<Empresa> Get()
        {
            return empresa.RetornarTodasAsEmpresas();
        }

        [HttpGet("Add")]
        public string Add(string cnpj,string nomeFantasia, string uf)
        {
            return empresa.CadastrarEmpresa(cnpj, nomeFantasia , uf);
        }

        [HttpGet("delete")]
        public string Delete(int id)
        {
            return empresa.ExcluirEmpresa(id);
        }
        
        [HttpGet("{id}")]
        public Empresa FindById(int id)
        {
            return empresa.RetornarEmpresaPorId(id);
        }
    }
}
