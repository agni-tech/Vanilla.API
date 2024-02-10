namespace Vanilla.Shared.Dtos;

public class AerodromoRedemetDataDto
{
    public string Localidade { get; set; }
    public string Nome { get; set; }
    public string Cidade { get; set; }
    public string Lon { get; set; }
    public string Lat { get; set; }
    public string Localizacao { get; set; }
    public string Metar { get; set; }
    public string Data { get; set; }
    public string Temperatura { get; set; }
    public string Ur { get; set; }
    public string Visibilidade { get; set; }
    public string Teto { get; set; }
    public string Ceu { get; set; }
    public string Condicoes_tempo { get; set; }
    public string TempoImagem { get; set; }
    public string Vento { get; set; }
}
public class AerodromoRedemetDto
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public AerodromoRedemetDataDto Data { get; set; }
}