using EngineerWorkSplace.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace EngineerWorkSplace.ViewModels
{
    internal class ServiceESPD_VM : BaseViewModel
    {
        private ESPD espd;

        public ServiceESPD_VM()
        {
            espd = new ESPD();
            StartMonitoring();
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
                    Console.WriteLine(EthernetAccess);
                }
            }
        }

        public async Task<bool> CheckEthernetAccessAsync()
        {
            try
            {
                using(var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync("8.8.8.8", 1000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ping failed: {ex.Message}"); // Вывод сообщения об ошибке
                return false;
            }
        }

        private async void StartMonitoring()
        {
            while (true)
            {
                EthernetAccess = await CheckEthernetAccessAsync();
                await Task.Delay(5000); // Повторная проверка каждые 5 секунд
            }
        }


        Ы
    }
}
