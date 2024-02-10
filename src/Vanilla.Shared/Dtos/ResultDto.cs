namespace Vanilla.Shared.Dtos
{
    public class ResultDto
    {
        public int? StatusCode { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }

        public ResultDto()
        {

        }
        public ResultDto(string message, object content, int status = 200)
        {
            StatusCode = status;
            Message = message;
            Content = content;
        }
    }
}
