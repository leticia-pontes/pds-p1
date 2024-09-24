// Ciência da Computação - 4° BCC A
// P1 (parte 1)
// Emerson Caique Alexandre Felizardo - 1976712
// Letícia Alves de Pontes - 1979942

using System.Globalization;

namespace P1_p1
{
    class Cliente
    {
        public string nome;
        public string email;
        public string cpf;
        public decimal valor;

        public Cliente(string nome, string email, string cpf, decimal valor)
        {
            this.nome = nome;
            this.email = email;
            this.cpf = cpf;
            this.valor = valor;
        }

        public void MostrarCliente()
        {
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"CPF: {cpf}");
            Console.WriteLine($"E-mail: {email}");
            Console.WriteLine($"Valor da Cobrança: {valor}\n");
        }
    }

    internal class Program
    {
        static void BuscaClientes(List<Cliente> listaClientes)
        {
            //CultureInfo ptbr = new CultureInfo("pt-BR");
            //NumberFormatInfo nfi = ptbr.NumberFormat;

            var arquivoRemessa = File.ReadAllLines("remessa.txt");

            foreach (var linha in arquivoRemessa) 
            {
                if (linha[0] == '9')
                {
                    string cpf = linha.Substring(1, 12);

                    string nome = linha.Substring(13, linha.IndexOf('&') - 13);
                    
                    string email = linha.Substring(linha.IndexOf('&') + 1, linha.IndexOf(' ')); 

                    decimal valor = Convert.ToDecimal(linha.Substring(linha.IndexOf('#') + 1));
                     //decimal valor = decimal.Parse(linha.Substring(linha.IndexOf('#') + 1), nfi);

                    Cliente cliente = new Cliente(nome, email, cpf, valor);
                    cliente.MostrarCliente();

                    listaClientes.Add(cliente);
                }
            }
        }

        static void SomaMediaValores(List<Cliente> listaClientes)
        {
            decimal soma = 0, media = 0;

            foreach (var cliente in listaClientes)
            {
                soma += cliente.valor;
            }

            media = soma / listaClientes.Count;

            Console.WriteLine($"Soma dos valores: {soma}");
            Console.WriteLine($"Média da cobrança: {media}\n");
        }

        static void VerificaValores(List<Cliente> listaClientes)
        {
            Console.WriteLine("Cobranças maiores que $5000");

            foreach (var cliente in listaClientes)
            {
                if (cliente.valor > 5000)
                {
                    Console.WriteLine($"{cliente.email}");
                }
            }
        }

        static void Main(string[] args)
        {
            List<Cliente> listaClientes = new List<Cliente>();

            BuscaClientes(listaClientes);
            SomaMediaValores(listaClientes);
            VerificaValores(listaClientes);
        }
    }
}