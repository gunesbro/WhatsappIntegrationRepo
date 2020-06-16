using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsappIntegration.Utility
{
    public class Enums
    {
        public const bool ChatMessageFromClient = true;
        public const bool ChatMessageFromUserAgent = false;

        public const bool SmartReplyFullMatch = true;
        public const bool SmartReplyContains = false;

        public const string ChatTypeWhatsapp = "Whatsapp";
        public const string ChatTypeSms = "Sms";
    }
}
