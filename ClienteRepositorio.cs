using System.Net.WebSockets;
using Cadastro;

namespace Repositorio;

public class ClienteRepositorio
{
    public List<Cliente> clientes = new List<Cliente>();

    public void ImprimirCliente(Cliente cliente)
    {
        Console.WriteLine("Id................: " + cliente.Id);
        Console.WriteLine("Nome..............: " + cliente.Nome);
        Console.WriteLine("Desconto..........: " + cliente.Desconto);
        Console.WriteLine("Data de Nascimento: " + cliente.DataNascimento);
        Console.WriteLine("Data de Cadastro..: " + cliente.CadastradoEm);
        Console.WriteLine("---------------------------------------------------");
    }

    public void ExibirClientes()
    {
        Console.Clear();
        foreach(var cliente in clientes)
        {
            ImprimirCliente(cliente);
        }
        Console.ReadKey();
    }

    public void CadastrarCLiente()
    {
        Console.Clear();

        Console.Write("Digite o nome do cliente: ");
        string nome = Console.ReadLine()!;
        Console.Write(Environment.NewLine);

        Console.Write("Digite a data de nascimento: ");
        DateOnly dataNascimento = DateOnly.Parse(Console.ReadLine()!);
        Console.Write(Environment.NewLine);

        Console.Write("Digite o desconto: ");
        decimal desconto = decimal.Parse(Console.ReadLine()!);
        Console.Write(Environment.NewLine);

        Cliente cliente = new Cliente();
        cliente.Id = clientes.Count + 1;
        cliente.Nome = nome!;
        cliente.Desconto = desconto;
        cliente.DataNascimento = dataNascimento;
        cliente.CadastradoEm = DateTime.Now;

        clientes.Add(cliente);

        ImprimirCliente(cliente);
        Console.WriteLine("Cliente cadastrado com sucesso! [Enter]");
        Console.ReadKey();
    }

    public void EditarCliente()
    {
        Console.Clear();
        Console.Write("Digite o código do cliente: ");
        var codigo = Console.ReadLine();

        var cliente = clientes.FirstOrDefault(c => c.Id == int.Parse(codigo!));

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado! [Enter]");
            Console.ReadKey();
            return;
        }

        ImprimirCliente(cliente);

        Console.Write("Digite o nome do cliente: ");
        string nome = Console.ReadLine()!;
        Console.Write(Environment.NewLine);

        Console.Write("Digite a data de nascimento: ");
        DateOnly dataNascimento = DateOnly.Parse(Console.ReadLine()!);
        Console.Write(Environment.NewLine);

        Console.Write("Digite o desconto: ");
        decimal desconto = decimal.Parse(Console.ReadLine()!);
        Console.Write(Environment.NewLine);

        //Cliente cliente = new Cliente();
        //cliente.Id = clientes.Count + 1;
        cliente.Nome = nome!;
        cliente.Desconto = desconto;
        cliente.DataNascimento = dataNascimento;
        //cliente.CadastradoEm = DateTime.Now;

        ImprimirCliente(cliente);
        Console.WriteLine("Cliente alterado com sucesso! [Enter]");
        Console.ReadKey();
    }

    public void ExcluirCliente()
    {
        Console.Clear();
        Console.Write("Informe o código do cliente a excluir: ");
        var codigo = Console.ReadLine();

        var cliente = clientes.FirstOrDefault(c => c.Id == int.Parse(codigo!));

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado! [Enter]");
            Console.ReadKey();
            return;
        }

        ImprimirCliente(cliente);

        clientes.Remove(cliente);

        Console.WriteLine("Cliente removido com sucesso! [Enter]");
        Console.ReadKey();
    }

    public void GravarDadosClientes()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(clientes);

        File.WriteAllText("clientes.txt", json);
    }

    public void LerDadosClientes()
    {
        if (File.Exists("clientes.txt"))
        {
            var dados = File.ReadAllText("clientes.txt");
        
            var clientesArquivo = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>(dados);

            clientes.AddRange(clientesArquivo!);
        }
    }
}