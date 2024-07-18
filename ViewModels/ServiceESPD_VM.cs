using EngineerWorkSplace.Models;
using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace EngineerWorkSplace.ViewModels
{
    internal class ServiceESPD_VM : BaseViewModel
    {
        private ESPD espd;
        private HttpClient httpClient;
        private HttpClient proxyClient;

        public ServiceESPD_VM()
        {
            espd = new ESPD();
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(5)
            };

            var httpClientHandler = new HttpClientHandler
            {
                Proxy = new System.Net.WebProxy("http://10.0.86.52:3128"),
                UseProxy = true
            };
            proxyClient = new HttpClient(httpClientHandler)
            {
                Timeout = TimeSpan.FromSeconds(5)
            };

            CertificatesVM = new Certificates_VM();

            StartMonitoring();
        }

        public Certificates_VM CertificatesVM { get; }

        public bool ServiceStatus
        {
            get => espd.ServiceStatus;
            set
            {
                if (espd.ServiceStatus != value)
                {
                    espd.ServiceStatus = value;
                    OnPropertyChanged(nameof(ServiceStatus));
                }
            }
        }

        public bool EthernetAccess
        {
            get => espd.EthernetAccess;
            set
            {
                if (espd.EthernetAccess != value)
                {
                    espd.EthernetAccess = value;
                    OnPropertyChanged(nameof(EthernetAccess));
                }
            }
        }

        public int PingESPD
        {
            get => espd.PingESPD;
            set
            {
                if (espd.PingESPD != value)
                {
                    espd.PingESPD = value;
                    OnPropertyChanged(nameof(PingESPD));
                }
            }
        }

        public bool ProxyAccess
        {
            get => espd.ProxyAccess;
            set
            {
                if (espd.ProxyAccess != value)
                {
                    espd.ProxyAccess = value;
                    OnPropertyChanged(nameof(ProxyAccess));
                }
            }
        }

        public int PingProxy
        {
            get => espd.PingProxy;
            set
            {
                if (espd.PingProxy != value)
                {
                    espd.PingProxy = value;
                    OnPropertyChanged(nameof(PingProxy));
                }
            }
        }

        public async Task<bool> CheckEthernetAccessAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("http://www.google.com");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CheckProxyAccessAsync()
        {
            try
            {
                var response = await proxyClient.GetAsync("http://www.google.com");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HTTP request via proxy failed: {ex.Message}");
                return false;
            }
        }

        public async Task<int> CheckPingEthernetAsync()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync("8.8.8.8", 1000);
                    if (reply.Status == IPStatus.Success)
                    {
                        return (int)reply.RoundtripTime;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ping failed: {ex.Message}");
            }
            return -1;
        }

        public async Task<int> CheckPingProxyAsync()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync("10.0.86.52", 1000);
                    if (reply.Status == IPStatus.Success)
                    {
                        return (int)reply.RoundtripTime;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ping proxy failed: {ex.Message}");
            }
            return -1;
        }

        private async void StartMonitoring()
        {
            while (true)
            {
                EthernetAccess = await CheckEthernetAccessAsync();
                PingESPD = await CheckPingEthernetAsync();
                ProxyAccess = await CheckProxyAccessAsync();
                PingProxy = await CheckPingProxyAsync();

                await Task.Delay(5000); // Повторная проверка каждые 5 секунд
            }
        }
    }
}
