using System;
using System.Net.Http;

namespace Persistance
{
    public abstract class BaseHttplClient : IDisposable
    {
        private static readonly object _locker = new();
        private static volatile HttpClient _client;

        public static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    lock (_locker)
                    {
                        if (_client == null)
                        {
                            HttpClientHandler _handler = new()
                            {
                                AllowAutoRedirect = false,
                                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                            };

                            _client = new HttpClient(_handler);
                        }
                    }
                }

                return _client;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client != null)
                {
                    _client.Dispose();
                }

                _client = null;
            }
        }

    }
}
