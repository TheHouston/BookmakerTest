

namespace Bookmaker
{
    public class Scope
    {
        /// <summary>
        /// Время жизни токена в часах.
        /// </summary>
        public const int LifeTime = 24;
        public const string ValidAudience = "Bookmaker";
        public const string ValidIssuer = "Bookmaker";
        /// <summary>
        /// Сокет платежной системы.
        /// </summary>
        public const string PaymentSystemUrl = "http://localhost:50002";
        public const string SecretKey = "XCAP05HgLoKvbRRa/QkadLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ=";
        public const string EncryptionKey = "Bookmaker";

    }
}
