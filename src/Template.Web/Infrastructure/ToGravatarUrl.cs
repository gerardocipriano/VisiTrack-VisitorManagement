using System.Security.Cryptography;
using System.Text;

namespace Template.Web.Infrastructure
{
    public static class ToGravatarUrlExtension
    {
        private const string gravatarImageUrl = "https://www.gravatar.com/avatar/{0}.jpeg";

        public static string ToGravatarUrl(this string email)
        {
            return ToGravatarUrl(email, DefaultGravatar.Automatic, null, null);
        }

        public static string ToGravatarUrl(this string email, DefaultGravatar defaultGravatar, int? width)
        {
            return ToGravatarUrl(email, defaultGravatar, width, null);
        }

        public enum DefaultGravatar
        {
            Automatic = 0,
            Custom = 1,
            Identicon = 2
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="defaultGravatar">null o url specifico o mp|identicon|monsterid|wavatar|retro|robohash|blank</param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string ToGravatarUrl(this string email, DefaultGravatar defaultGravatar, int? width, bool? forceDefault)
        {
            var url = string.Format(gravatarImageUrl, ToHashEmailForGravatar(email));
            url += "?";
            var first = true;

            switch (defaultGravatar)
            {
                case DefaultGravatar.Automatic:
                    break;
                case DefaultGravatar.Custom:
                    if (width != null)
                    {
                        url += "s=" + width;
                        first = false;
                    }
                    break;
                case DefaultGravatar.Identicon:
                    url += "d=identicon";
                    first = false;
                    break;
            }

            if (forceDefault != null)
            {
                if (!first)
                    url += "&";

                url += "f=y";
            }

            return url;
        }

        /// Hashes an email with MD5.  Suitable for use with Gravatar profile
        /// image urls
        private static string ToHashEmailForGravatar(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return "00000000000000000000000000000000";

            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();  // Return the hexadecimal string. 
        }
    }
}