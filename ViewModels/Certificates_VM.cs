using EngineerWorkSplace.Models;
using System.Collections.ObjectModel;

namespace EngineerWorkSplace.ViewModels
{
    internal class Certificates_VM : BaseViewModel
    {
        public ObservableCollection<Certificates> CertificatesList { get; private set; }

        public Certificates_VM()
        {
            CertificatesList = new ObservableCollection<Certificates>();
        }

        public void AddCertificate(Certificates certificate)
        {
            CertificatesList.Add(certificate);
        }

        public ObservableCollection<Certificates> GetCertificates()
        {
            return CertificatesList;
        }
    }
}
