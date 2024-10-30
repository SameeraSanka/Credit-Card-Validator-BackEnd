namespace CreditCardValidator_API.Common
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public string Data { get; set; }
    }
}
