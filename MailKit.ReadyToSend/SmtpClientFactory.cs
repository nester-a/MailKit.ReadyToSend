using MailKit.Net.Smtp;

namespace MailKit.ReadyToSend
{
    public class SmtpClientFactory
    {
        private readonly SmtpClientOptions options;

        public SmtpClientFactory(SmtpClientOptions options)
        {
            this.options = options;
        }

        public SmtpClient Create()
        {
            var client = new SmtpClient();
            client.Connect(options.Host, options.Port, options.UseSsl);
            
            if(options.Username != null && options.Password != null)
            {
                client.Authenticate(options.Username, options.Password);
            }

            return client;
        }

        public async Task<SmtpClient> CreateAsync(CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            var client = new SmtpClient();
            await client.ConnectAsync(options.Host, options.Port, options.UseSsl, cancellation);

            if (options.Username != null && options.Password != null)
            {
                await client.AuthenticateAsync(options.Username, options.Password, cancellation);
            }

            return client;
        }
    }
}
