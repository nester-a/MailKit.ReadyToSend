using MimeKit;

namespace MailKit.ReadyToSend
{
    public class MailKitEmailSender
    {
        private readonly SmtpClientFactory smtpClientFactory;

        public MailKitEmailSender(SmtpClientFactory smtpClientFactory)
        {
            this.smtpClientFactory = smtpClientFactory;
        }

        public async Task SendAsync(string from, string to, string subject, string textbody, string? htmlBody, string[]? attacmentFilesPaths, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await SendAsync(from, new string[] { to }, subject, textbody, htmlBody, attacmentFilesPaths, cancellation);
        }

        public async Task SendAsync(string from, string[] to, string subject, string textbody, string? htmlBody, string[]? attacmentFilesPaths, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            var fromMailbox = new MailboxAddress(from, from);

            var toMailboxes = to.Select(mail => new MailboxAddress(mail, mail))
                                .ToArray();

            var builder = new BodyBuilder();

            builder.AddTextBody(textbody);

            if (!string.IsNullOrWhiteSpace(htmlBody))
            {
                builder = builder.AddHtmlBody(htmlBody);
            }

            if (attacmentFilesPaths is not null && attacmentFilesPaths.Any())
            {
                builder = builder.AddAttachments(attacmentFilesPaths);
            }

            var messageBody = builder.ToMessageBody();

            await SendAsync(fromMailbox, toMailboxes, subject, messageBody, cancellation);
        }

        public async Task SendAsync(string from, string to, string subject, string body, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await SendAsync(from, new string[] { to }, subject, body, cancellation);
        }

        public async Task SendAsync(string from, string[] to, string subject, string body, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            var fromMailbox = new MailboxAddress(from, from);

            var toMailboxes = to.Select(mail => new MailboxAddress(mail, mail))
                                .ToArray();

            var messageBody = new TextPart("plain")
            {
                Text = body,
            };

            await SendAsync(fromMailbox, toMailboxes, subject, messageBody, cancellation);
        }

        public async Task SendAsync(MailboxAddress from, MailboxAddress to, string subject, MimeEntity messageBody, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await SendAsync(from, new MailboxAddress[] { to }, subject, messageBody, cancellation);
        }

        public async Task SendAsync(MailboxAddress from, MailboxAddress[] to, string subject, MimeEntity messageBody, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            var message = new MimeMessage(new InternetAddress[] { from }, to, subject, messageBody);

            await SendAsync(message, cancellation);
        }

        public async Task SendAsync(MimeMessage message, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            using var client = await smtpClientFactory.CreateAsync(cancellation);

            await client.SendAsync(message, cancellation);
            await client.DisconnectAsync(true, cancellation);
        }
    }
}