using EngineerWorkSplace.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace EngineerWorkSplace.ViewModels
{
    public class Certificates_VM : BaseViewModel
    {
        private string pathToCertificates;

        public ObservableCollection<Certificates> CertificatesList { get; set; }

        public Certificates_VM()
        {
            CertificatesList = new ObservableCollection<Certificates>();
        }

        public string PathToCertificates
        {
            get => pathToCertificates;
            set
            {
                if (pathToCertificates != value)
                {
                    pathToCertificates = value;
                    OnPropertyChanged(nameof(PathToCertificates));
                    LoadCertificatesFromPath();
                }
            }
        }

        private void LoadCertificatesFromPath()
        {
            if (!Directory.Exists(pathToCertificates))
                return;

            CertificatesList.Clear();

            var directories = Directory.GetDirectories(pathToCertificates);
            foreach (var dir in directories)
            {
                var files = Directory.GetFiles(dir, "*.cer"); // Assuming .cer files
                foreach (var file in files)
                {
                    try
                    {
                        var cert = new X509Certificate2(file);
                        var certificate = new Certificates
                        {
                            FullName = ExtractFullName(cert.Subject),
                            Position = ExtractPosition(cert.Subject),
                            StartTime = cert.NotBefore,
                            EndTime = cert.NotAfter
                        };
                        CertificatesList.Add(certificate);
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        Console.WriteLine($"Failed to load certificate from {file}: {ex.Message}");
                    }
                }
            }
        }

        private string ExtractFullName(string subject)
        {
            return ExtractValueFromSubject(subject, "CN=");
        }

        private string ExtractPosition(string subject)
        {
            return ExtractValueFromSubject(subject, "T=");
        }

        private string ExtractValueFromSubject(string subject, string key)
        {
            int startIndex = subject.IndexOf(key);
            if (startIndex == -1)
            {
                return "Unknown";
            }
            startIndex += key.Length;
            int endIndex = subject.IndexOf(",", startIndex);
            if (endIndex == -1)
            {
                endIndex = subject.Length;
            }
            return subject.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}
