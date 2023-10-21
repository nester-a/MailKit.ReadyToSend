using MimeKit;

namespace MailKit.ReadyToSend
{
    public static class BodyBuilderExtensions
    {
        public static BodyBuilder AddTextBody(this BodyBuilder builder, string text)
        {
            builder.TextBody = text;
            return builder;
        }

        public static BodyBuilder AddHtmlBody(this BodyBuilder builder, string html)
        {
            builder.HtmlBody = html;
            return builder;
        }

        public static BodyBuilder AddAttachment(this BodyBuilder builder, string attachmentFilePath)
        {
            builder.Attachments.Add(attachmentFilePath);
            return builder;
        }

        public static BodyBuilder AddAttachments(this BodyBuilder builder, params string[] attachmentFilePath)
        {
            foreach(var attach in attachmentFilePath)
            {
                builder.AddAttachment(attach);
            }

            return builder;
        }
    }
}
