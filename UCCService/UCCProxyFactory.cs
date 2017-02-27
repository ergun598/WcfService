using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.IO;
using WinFormsTest.nci;
using WinFormsTest.ucc;
using System.Web.Hosting;

namespace UCCService
{
    internal static class UCCProxyFactory
    {
        private static X509Certificate2 clientCertificate = null;
        private static X509Certificate2 serviceCertificate = null;
        private static X509CertificateEndpointIdentity endpointIdentity = null;

        public static string ClientCertificatePath { get; set; }
        public static string ServiceCertificatePath { get; set; }
        public static string ClientCertificatePassword { get; set; }

        public static NCIServiceWCFClient CreateNCIServiceClient(Uri uri)
        {
            UCCProxyFactory.ClientCertificatePath = HostingEnvironment.MapPath(@"~/App_Data/isbank_test_private.pfx");
            UCCProxyFactory.ServiceCertificatePath = HostingEnvironment.MapPath(@"~/App_Data/ucc_test_public.cer");
            UCCProxyFactory.ClientCertificatePassword = "111";
            
            System.Net.ServicePointManager.Expect100Continue = false;

            if (string.IsNullOrEmpty(ClientCertificatePath) || string.IsNullOrEmpty(ServiceCertificatePath))
            {
                throw new InvalidOperationException("You should specify certificates path first");
            }

            if (string.IsNullOrEmpty(ClientCertificatePassword))
            {
                throw new InvalidOperationException("You should specify ClientCertificatePassword");
            }

            clientCertificate = new X509Certificate2(ClientCertificatePath, ClientCertificatePassword);
            serviceCertificate = new X509Certificate2(ServiceCertificatePath);

            endpointIdentity = new X509CertificateEndpointIdentity(serviceCertificate, new X509Certificate2Collection(clientCertificate));
            EndpointAddress ea = new EndpointAddress(uri, endpointIdentity);

            CustomBinding cb = new CustomBinding();
            cb.CloseTimeout = new TimeSpan(50000000);
            TextMessageEncodingBindingElement messageBindingElement = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);
            HttpTransportBindingElement nciTransport = new HttpTransportBindingElement();
            nciTransport.MaxReceivedMessageSize = 5000000;//115000000;
            messageBindingElement.ReaderQuotas.MaxStringContentLength = 1200000;//11200000;
            AsymmetricSecurityBindingElement abe = SecurityBindingElement.CreateMutualCertificateDuplexBindingElement(MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10); ;
            abe.AllowSerializedSigningTokenOnReply = true;
            abe.MessageProtectionOrder = System.ServiceModel.Security.MessageProtectionOrder.SignBeforeEncrypt;
            abe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Basic128Rsa15;
            abe.SetKeyDerivation(false);
            cb.Elements.Add(abe);
            cb.Elements.Add(messageBindingElement);
            cb.Elements.Add(nciTransport);
            NCIServiceWCFClient nciClient = new NCIServiceWCFClient(cb, ea);
            nciClient.ClientCredentials.ServiceCertificate.DefaultCertificate = serviceCertificate;
            nciClient.ClientCredentials.ClientCertificate.Certificate = clientCertificate;
            nciClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

            return nciClient;
        }

        public static UCCServicesWCFClient CreateUCCServiceClient(Uri uri)
        {

            UCCProxyFactory.ClientCertificatePath = HostingEnvironment.MapPath(@"~/App_Data/isbank_test_private.pfx");
            UCCProxyFactory.ServiceCertificatePath = HostingEnvironment.MapPath(@"~/App_Data/ucc_test_public.cer");
            UCCProxyFactory.ClientCertificatePassword = "111";
            
            
            
            System.Net.ServicePointManager.Expect100Continue = false;
            if (string.IsNullOrEmpty(ClientCertificatePath) || string.IsNullOrEmpty(ServiceCertificatePath))
            {
                throw new InvalidOperationException("You should specify certificates path first");
            }

            if (string.IsNullOrEmpty(ClientCertificatePassword))
            {
                throw new InvalidOperationException("You should specify ClientCertificatePassword");
            }

            clientCertificate = new X509Certificate2(ClientCertificatePath, ClientCertificatePassword);
            serviceCertificate = new X509Certificate2(ServiceCertificatePath);

            endpointIdentity = new X509CertificateEndpointIdentity(serviceCertificate, new X509Certificate2Collection(clientCertificate));
            EndpointAddress ea = new EndpointAddress(uri, endpointIdentity);

            CustomBinding cb = new CustomBinding();
            TextMessageEncodingBindingElement messageBindingElement = new TextMessageEncodingBindingElement(MessageVersion.Soap11, Encoding.UTF8);
            HttpTransportBindingElement uccTransport = new HttpTransportBindingElement();
            uccTransport.MaxReceivedMessageSize = 115000000;//115000000;
            messageBindingElement.ReaderQuotas.MaxStringContentLength = 11200000;//11200000;
            AsymmetricSecurityBindingElement abe = SecurityBindingElement.CreateMutualCertificateDuplexBindingElement(MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10); ;
            abe.AllowSerializedSigningTokenOnReply = true;
            abe.MessageProtectionOrder = System.ServiceModel.Security.MessageProtectionOrder.SignBeforeEncrypt;
            abe.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Basic128Rsa15;
            abe.SetKeyDerivation(false);
            cb.Elements.Add(abe);
            cb.Elements.Add(messageBindingElement);
            cb.Elements.Add(uccTransport);
            UCCServicesWCFClient uccClient = new UCCServicesWCFClient(cb, ea);
            uccClient.ClientCredentials.ServiceCertificate.DefaultCertificate = serviceCertificate;
            uccClient.ClientCredentials.ClientCertificate.Certificate = clientCertificate;
            uccClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

            return uccClient;
        }
    }
}
