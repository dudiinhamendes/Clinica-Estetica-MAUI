namespace SistemaClinica.Models;

public class Agendamento
{
    public int Id_agendamento { get; set; }

    public string Nome_cliente { get; set; }

    public string Nome_funcionario { get; set; }

    public string Nome_servico { get; set; }

    public DateTime Data_Horario { get; set; }

    public string Status { get; set; }

    public Agendamento()
    {
    }

    public Agendamento(int id, string nomeCliente, string nomeFuncionario, string nomeServico, DateTime dataHorario, string status)
    {
        Id_agendamento = id;
        Nome_cliente = nomeCliente;
        Nome_funcionario = nomeFuncionario;
        Nome_servico = nomeServico;
        Data_Horario = dataHorario;
        Status = status;
    }
}