using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WinFormsTest.nci;
using WinFormsTest.ucc;



namespace UCCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : NCIServiceWCF, UCCServices
    {

        public executeResponse execute(execute request)
        {



            string debt = "";
            try
            {
                //init data
                WinFormsTest.nci.execute ex = new WinFormsTest.nci.execute();
                WinFormsTest.nci.Parameter[] prs = new WinFormsTest.nci.Parameter[request.parameter.Length];
                int count = 0;
                foreach (Parameter param in request.parameter)
                {
                    prs[count] = new WinFormsTest.nci.Parameter();
                    prs[count].key = param.key;//"abonentCode";
                    prs[count].value = param.value;
                    count++;
                }

                ex.parameter = prs;
                ex.serviceId = request.serviceId;

               // WinFormsTest.nci.NCIServiceWCFClient ee = UCCProxyFactory.CreateNCIServiceClient(new Uri("http://0330X395:8089/nci/NCIServiceWCFBean"));
                   WinFormsTest.nci.NCIServiceWCFClient ee = UCCProxyFactory.CreateNCIServiceClient(new Uri("http://92.241.79.133:8080/nci/NCIServiceWCFBean")); 
                //nci.NCIServiceWCFClient ee = UCCProxyFactory.CreateNCIServiceClient(new Uri("http://10.87.12.181:7000/ExternalServices/UCCNCIService"));
                WinFormsTest.nci.executeResponse resp = new WinFormsTest.nci.executeResponse();

                executeResponse rtn = new executeResponse();
                //execute
                try
                {

                    resp = ee.execute(ex);



                    StringBuilder sb = new StringBuilder();

                    if (resp.outParameter != null)
                    {
                        rtn.outParameter = new Parameter[resp.outParameter.Length];
                        count = 0;
                        foreach (WinFormsTest.nci.Parameter param in resp.outParameter)
                        {
                            //System.Diagnostics.Trace.WriteLine(param.key + " " + param.value); 
                            rtn.outParameter[count] = new Parameter();
                            rtn.outParameter[count].key = param.key;
                            rtn.outParameter[count].value = param.value;
                            sb.AppendFormat("{0}={1}\r\n", param.key, param.value);
                            count++;
                        }
                    }

                    if (resp.faultEntry != null)
                    {
                        rtn.faultEntry = new FaultEntry[resp.faultEntry.Length];
                        count = 0;
                        foreach (WinFormsTest.nci.FaultEntry fe in resp.faultEntry)
                        {
                            //System.Diagnostics.Trace.WriteLine(param.key + " " + param.value);                    
                            rtn.faultEntry[count] = new FaultEntry();
                            rtn.faultEntry[count].errorKey = fe.errorKey;
                            rtn.faultEntry[count].invalidParameter = fe.invalidParameter;
                            rtn.faultEntry[count].errorParameters = fe.errorParameters;
                            sb.AppendFormat("{0}={1}\r\n", "error", fe.errorKey);
                            count++;
                        }
                    }


                }
                catch (Exception exc)
                {
                    //System.Diagnostics.Debug.WriteLine(exc.Message)
                    return null;
                }

                return rtn;

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return null;
        }
        public bulkExecuteResponse bulkExecute(bulkExecute request)
        {

            string debt = "";
            try
            {
                //init data
                WinFormsTest.nci.bulkExecuteRequest nciex = new WinFormsTest.nci.bulkExecuteRequest();
               
                int count = 0;
                nciex.bulkExecute = new WinFormsTest.nci.ExecuteRequest[request.executeRequest.Length];
                foreach (ExecuteRequest exect in request.executeRequest)
                {
                    WinFormsTest.nci.Parameter[] prs = new WinFormsTest.nci.Parameter[exect.parameter.Length];
                   
                    int countpa = 0;
                    foreach (Parameter param in exect.parameter)
                    {
                        prs[count] = new WinFormsTest.nci.Parameter();
                        prs[count].key = param.key;//"abonentCode";
                        prs[count].value = param.value;
                        countpa++;
                    }
                    WinFormsTest.nci.ExecuteRequest nciexect = new WinFormsTest.nci.ExecuteRequest();
                    nciexect.parameter = prs;
                    nciexect.serviceId = exect.serviceId;


                    nciex.bulkExecute[count] = nciexect;
                    count++;
                }



                //   WinFormsTest.nci.NCIServiceWCFClient ee = UCCProxyFactory.CreateNCIServiceClient(new Uri("http://localhost:8089/nci/NCIServiceWCFBean"));
                 WinFormsTest.nci.NCIServiceWCFClient ee = UCCProxyFactory.CreateNCIServiceClient(new Uri("http://92.241.79.133:8080/nci/NCIServiceWCFBean")); 
                //nci.NCIServiceWCFClient ee = UCCProxyFactory.CreateNCIServiceClient(new Uri("http://10.87.12.181:7000/ExternalServices/UCCNCIService"));
                WinFormsTest.nci.ExecuteResult[] resp;

                bulkExecuteResponse rtn = new bulkExecuteResponse();
                //execute
                try
                {

                    resp = ee.bulkExecute(nciex.bulkExecute);
                    


                }
                catch (Exception exc)
                {
                    //System.Diagnostics.Debug.WriteLine(exc.Message)
                    return null;
                }

                return rtn;

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return null;
        }
        public getServiceProvidersResponse getServiceProviders(getServiceProvidersRequest request)
        {

            try
            {
                WinFormsTest.ucc.getServiceProvidersRequest ex = new WinFormsTest.ucc.getServiceProvidersRequest();
                ex.getServiceProviders = new WinFormsTest.ucc.getServiceProviders();
                WinFormsTest.ucc.UCCServicesWCF ee = UCCProxyFactory.CreateUCCServiceClient(new Uri("http://92.241.79.133:8080/ucc-services/UCCServicesWCFBean"));
                WinFormsTest.ucc.getServiceProvidersResponse1 resp = new WinFormsTest.ucc.getServiceProvidersResponse1();

                getServiceProvidersResponse rtn = new getServiceProvidersResponse();
               
                //execute
                try
                {

                    resp = ee.getServiceProviders(ex);
                    rtn.getServiceProvidersResponse1 = new ServiceProvider[resp.getServiceProvidersResponse.serviceProviders.Length];
                    int count=0;
                  foreach(WinFormsTest.ucc.ServiceProvider param in resp.getServiceProvidersResponse.serviceProviders)
                  {
                      rtn.getServiceProvidersResponse1[count] = new ServiceProvider();
                      rtn.getServiceProvidersResponse1[count].address = param.address;
                      rtn.getServiceProvidersResponse1[count].code = param.code;
                      rtn.getServiceProvidersResponse1[count].contactPerson = param.contactPerson;
                      rtn.getServiceProvidersResponse1[count].email = param.email;
                      rtn.getServiceProvidersResponse1[count].fax = param.fax;
                      rtn.getServiceProvidersResponse1[count].name = param.name;
                      rtn.getServiceProvidersResponse1[count].phone = param.phone;
                      rtn.getServiceProvidersResponse1[count].registrationCode = param.registrationCode;
                     // rtn.getServiceProvidersResponse1[count].Service = param.Service;
                    //  rtn.getServiceProvidersResponse1[count].ServiceProviderAccount = param.ServiceProviderAccount;
                      rtn.getServiceProvidersResponse1[count].shortName = param.shortName;
                      rtn.getServiceProvidersResponse1[count].ServiceProviderAccount = new ServiceProviderAccount[param.ServiceProviderAccount.Length];
                       int countSer=0;
                       foreach (WinFormsTest.ucc.ServiceProviderAccount servs in param.ServiceProviderAccount)
                       {
                           rtn.getServiceProvidersResponse1[count].ServiceProviderAccount[countSer] = new ServiceProviderAccount();
                           rtn.getServiceProvidersResponse1[count].ServiceProviderAccount[countSer].active = servs.active;
                           rtn.getServiceProvidersResponse1[count].ServiceProviderAccount[countSer].bankAccountName = servs.bankAccountName;
                           rtn.getServiceProvidersResponse1[count].ServiceProviderAccount[countSer].bankAccountNumber = servs.bankAccountNumber;
                           rtn.getServiceProvidersResponse1[count].ServiceProviderAccount[countSer].bankCode = servs.bankCode;
                           int countSercomTrea = 0;
                           if (servs.TreasuryCodes != null)
                           {
                               rtn.getServiceProvidersResponse1[count].ServiceProviderAccount[countSer].TreasuryCodes = new TreasuryCode[servs.TreasuryCodes.Length];
                               foreach (WinFormsTest.ucc.TreasuryCode servsComTreas in servs.TreasuryCodes)
                               {
                                   rtn.getServiceProvidersResponse1[count].ServiceProviderAccount[countSer].TreasuryCodes[countSercomTrea] = new TreasuryCode();
                                   rtn.getServiceProvidersResponse1[count].ServiceProviderAccount[countSer].TreasuryCodes[countSercomTrea].code = servsComTreas.code;
                                   rtn.getServiceProvidersResponse1[count].ServiceProviderAccount[countSer].TreasuryCodes[countSercomTrea].description = servsComTreas.description;
                                   countSercomTrea++;
                               }

                           }
                       
                           countSer++;
                       }
                       
                      countSer=0;
                      rtn.getServiceProvidersResponse1[count].Service = new Service[param.Service.Length];
                      foreach(WinFormsTest.ucc.Service servs in param.Service)
                      {
                          rtn.getServiceProvidersResponse1[count].Service[countSer] = new Service();
                          rtn.getServiceProvidersResponse1[count].Service[countSer].abonentDetailsPattern = servs.abonentDetailsPattern;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].active = servs.active;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].additionalInfo = servs.additionalInfo;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].allAgentAccounts = servs.allAgentAccounts;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].cancelServiceCode = servs.cancelServiceCode;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].debtServiceCode = servs.debtServiceCode;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].description = servs.description;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].immediateTransaction = servs.immediateTransaction;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].oneTimePayment = servs.oneTimePayment;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].payServiceCode = servs.payServiceCode;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].phoneAbonentCode = servs.phoneAbonentCode;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCode = servs.serviceCode;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].serviceIcon = servs.serviceIcon;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].transferCommissions = servs.transferCommissions;
                          rtn.getServiceProvidersResponse1[count].Service[countSer].verifyServiceCode = servs.verifyServiceCode;
                 //         rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts = servs.serviceAccounts;

                           int countSercom=0;
                           rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions = new ServiceCommission[servs.serviceCommissions.Length];
                           foreach (WinFormsTest.ucc.ServiceCommission servsCom in servs.serviceCommissions)
                           {
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions[countSercom]=new ServiceCommission();
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions[countSercom].commissionFixed = servsCom.commissionFixed;
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions[countSercom].commissionPercent = servsCom.commissionPercent;
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions[countSercom].currency = servsCom.currency;
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions[countSercom].maxAmount = servsCom.maxAmount;
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions[countSercom].maxCommission = servsCom.maxCommission;
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions[countSercom].minAmount = servsCom.minAmount;
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions[countSercom].minCommission = servsCom.minCommission;
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceCommissions[countSercom].paymentChannel = servsCom.paymentChannel;
                               countSercom++;
                           }
                           if (servs.serviceAccounts != null)
                           {
                               countSercom = 0;
                               rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts = new ServiceProviderAccount[servs.serviceAccounts.Length];
                               foreach (WinFormsTest.ucc.ServiceProviderAccount servsCom in servs.serviceAccounts)
                               {
                                   rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts[countSercom] = new ServiceProviderAccount();
                                   rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts[countSercom].active = servsCom.active;
                                   rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts[countSercom].bankAccountName = servsCom.bankAccountName;
                                   rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts[countSercom].bankAccountNumber = servsCom.bankAccountNumber;
                                   rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts[countSercom].bankCode = servsCom.bankCode;

                                   if (servsCom.TreasuryCodes != null)
                                   {

                                       int countSercomTrea = 0;
                                       rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts[countSercom].TreasuryCodes = new TreasuryCode[servsCom.TreasuryCodes.Length];
                                       foreach (WinFormsTest.ucc.TreasuryCode servsComTreas in servsCom.TreasuryCodes)
                                       {
                                           rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts[countSercom].TreasuryCodes[countSercomTrea] = new TreasuryCode();
                                           rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts[countSercom].TreasuryCodes[countSercomTrea].code = servsComTreas.code;
                                           rtn.getServiceProvidersResponse1[count].Service[countSer].serviceAccounts[countSercom].TreasuryCodes[countSercomTrea].description = servsComTreas.description;
                                           countSercomTrea++;
                                       }
                                       countSercom++;
                                   }
                               }
                           }
                           countSer++;
                      }
                      count++;
                  }


                }
                catch (Exception exc)
                {
                    //System.Diagnostics.Debug.WriteLine(exc.Message)
                    return null;
                }

                return rtn;
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return null;

        }

        public getCommissionResponse1 getCommission(getCommissionRequest request)
        {

            return null;
        }

        public getPaymentResponse1 getPayment(getPaymentRequest request)
        {

            return null;
        }

        public getPaymentsResponse getPayments(getPaymentsRequest request)
        {

            return null;
        }

        public reconcileResponse1 reconcile(reconcileRequest request)
        {

            return null;
        }

        public registerClearingOrderResponse1 registerClearingOrder(registerClearingOrderRequest request)
        {

            return null;
        }
    }
}
