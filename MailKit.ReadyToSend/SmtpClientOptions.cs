namespace MailKit.ReadyToSend
{
    public class SmtpClientOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
