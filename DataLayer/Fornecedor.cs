using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer
{
    public class Fornecedor
    {
        public int FornecedorID { get; set; }

        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public List<Telefone> Telefones { get; set; }
        public string RG { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
