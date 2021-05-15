using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer
{
    public class Telefone
    {
        public int TelefoneID { get; set; }
        public string NumeroTelefone { get; set; }
        public int FornecedorId { get; set; }
        
        [JsonIgnore]
        public Fornecedor Fornecedor { get; set; }
    }
}
