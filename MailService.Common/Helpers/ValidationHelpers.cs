using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace MailService.Common.Helpers
{
    public class ValidationHelpers
    {
        public static string[] ValidMediaTypes = new[] {
            MediaTypeNames.Application.Octet,
            MediaTypeNames.Application.Pdf,
            MediaTypeNames.Application.Rtf,
            MediaTypeNames.Application.Soap,
            MediaTypeNames.Application.Zip,
            MediaTypeNames.Image.Gif,
            MediaTypeNames.Image.Jpeg,
            MediaTypeNames.Image.Tiff,
            MediaTypeNames.Text.Html,
            MediaTypeNames.Text.Plain,
            MediaTypeNames.Text.RichText,
            MediaTypeNames.Text.Xml,
        };

        public static bool BeValidEncoding(string encodingString)
        {
            try
            {
                Encoding.GetEncoding(encodingString);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool BeValidMediaType(string mediaType)
        {
            return ValidMediaTypes.Contains(mediaType);
        }
    }
}
