namespace ApiBlibliotecaSimples.DTOs;

public class DtoResponseEmprestimoComItens
{
    public int Id { get; set; }
    public DateOnly DataEmprestimo { get; set; }
    public DateOnly PrevisaoDevolucao { get; set; }
    public string Status { get; set; }
    public int ClienteId { get; set; }
    public List<DtoResponseItemEmprestimo> Itens { get; set; } = new List<DtoResponseItemEmprestimo>();
}
