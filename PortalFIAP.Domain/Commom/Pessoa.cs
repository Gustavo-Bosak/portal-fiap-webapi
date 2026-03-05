namespace PortalFIAP.Domain.Commom;

public abstract class Pessoa : BaseEntity
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    private DateOnly DataNasc { get; set; }
    public string Telefone { get; private set; }
    public string Endereco { get; private set; }
    

    public Pessoa(string nome, string email, DateOnly dataNascimento, string telefone, string endereco)
    {
        DefinirNome(nome);
        DefinirEmail(email);
        DefinirDataNasc(dataNascimento);
        DefinirTelefone(telefone);
        Endereco = endereco;
    }
    
    //Funcao Definir/Validar nome
    public void DefinirNome(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new Exception("Nome não pode ser vazio.");
        
        Nome = newName;
    }
    
    //Funcao Definir/Validar Email
    public void DefinirEmail(string novoEmail)
    {
        if (string.IsNullOrWhiteSpace(novoEmail) || !novoEmail.Contains("@"))
            throw new Exception("E-mail inválido.");
            
        Email = novoEmail;
    }
    
    //Funcao Validar/Calcular Data Nasci
    public void DefinirDataNasc(DateOnly novaData)
    {
        var idade = CalculaIdade(novaData);
        
        if (idade < 16)
            throw new Exception("Usuário deve ter pelo menos 16 anos.");

        DataNasc = novaData;
    }
    
    public int Idade => CalculaIdade(DataNasc);
    
    private static int CalculaIdade(DateOnly data)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var idade = today.Year - data.Year;
        if (data > today.AddYears(idade))idade--;
        return idade;
    }

    //Funcao Definir/Validar Telefone
    public void DefinirTelefone(string novoTelefone)
    {
        if (string.IsNullOrWhiteSpace(novoTelefone))
            throw new Exception("Telefone nao pode estar vazio.");
            
        Telefone = novoTelefone;
    }
    
    
}
