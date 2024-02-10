namespace Vanilla.Shared.Dtos;

public class EmailSettingsDto
{
    public string Smtp { get; set; }
    public string Pwd { get; set; }
    public string From { get; set; }
    public string Credencial { get; set; }
    public bool Ssl { get; set; }
    public int Port { get; set; }
}
