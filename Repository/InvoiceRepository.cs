﻿using CrudReportGenerate.Interface;
using CrudReportGenerate.Model.Common;
using CrudReportGenerate.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudReportGenerate.Repository
{
    public class InvoiceRepository : IInvoice
    {
        string Val;
        public List<Model.Common.Invoice> GetInvoice()
        {
            List<Model.Common.Invoice> Items = new List<Model.Common.Invoice>();
            try
            {
                using (var dBContext = new CustomerReportContext())
                {
                    Model.Common.Invoice inv;
                    foreach (var it in dBContext.TblInvoices.ToList())
                    {
                        var customer = dBContext.TblCustomer.FirstOrDefault(x => x.CustomerNo == it.CustomerNo);

                        inv = new Model.Common.Invoice();
                        inv.InvoiceNo = it.InvoiceNo;
                        inv.CustomerNo = it.CustomerNo;
                        if (customer != null)
                        {
                            inv.CustomerName = customer.CustomerName;
                        }

                        inv.InvoiceDate = it.InvoiceDate;
                        inv.InvoiceAmount = it.InvoiceAmount;
                        inv.PaymentDueDate = it.PaymentDueDate;
                        Items.Add(inv);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Items;
        }

        public int AddInvoiceData(Model.Common.Invoice InvoiceModel, string InvoiceNo)
        {
            List<Model.Common.Invoice> Items = new List<Model.Common.Invoice>();

            int returnVal = 0;
            try
            {
                using (var dBContext = new CustomerReportContext())
                {
                    Model.Common.Invoice get;
                    foreach (var it in dBContext.TblInvoices)
                    {
                        get = new Model.Common.Invoice();
                        get.InvoiceNo = it.InvoiceNo;
                        get.CustomerNo = it.CustomerNo;
                        get.InvoiceDate = it.InvoiceDate;
                        get.InvoiceAmount = it.InvoiceAmount;
                        get.PaymentDueDate = it.PaymentDueDate;
                        get.CreatedBy = it.CreatedBy;
                        get.CreatedDate = it.CreatedDate;
                        Items.Add(get);
                    }

                    TblInvoices Cust;
                    //Add record
                    Cust = new TblInvoices();
                    Cust.InvoiceNo = InvoiceModel.InvoiceNo;
                    Cust.CustomerNo = InvoiceModel.CustomerNo;
                    Cust.InvoiceDate = InvoiceModel.InvoiceDate;
                    Cust.InvoiceAmount = InvoiceModel.InvoiceAmount;
                    Cust.PaymentDueDate = Cust.InvoiceDate.AddDays(30);
                    Cust.CreatedBy = InvoiceModel.CreatedBy;
                    Cust.CreatedDate = DateTime.Now;
                    InvoiceNo = Cust.InvoiceNo;

                    bool Inv = Items.Any(asd => asd.InvoiceNo == InvoiceNo);
                    if (Inv == true)
                    {
                        returnVal = -1;
                    }

                    if (Inv == false)
                    {
                        dBContext.TblInvoices.Add(Cust);
                        returnVal = dBContext.SaveChanges();
                    }

                    //Add AutoIncreament

                    AutoIncrement Auto = new AutoIncrement();

                    int Num = Convert.ToInt32(Cust.InvoiceNo.Substring(1));
                    string Num1 = Convert.ToString(Num);
                    foreach (var val in dBContext.AutoIncrement.ToList())
                    {
                        Auto.AutoCustomerNo = val.AutoCustomerNo;
                        Auto.AutoInvoiceNo = Num;
                        Auto.AutoPaymentNo = val.AutoPaymentNo;
                    }

                    var rows = from cu in dBContext.AutoIncrement select cu;

                    foreach (var row in rows)
                    {
                        if (row != null)
                        {
                            dBContext.AutoIncrement.Remove(row);
                            //dbcontext.savechanges();
                        }
                    }

                    dBContext.AutoIncrement.Add(Auto);

                    returnVal = dBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return returnVal;
        }

        public int UpdateInvoice(Invoice InvoiceModel, string InvoiceNo, string InvoiceName)
        {
            List<Invoice> Items = new List<Invoice>();

            int returnVal = 0;
            try
            {
                using (var dBContext = new CustomerReportContext())
                {
                    Invoice get;
                    foreach (var it in dBContext.TblInvoices)
                    {
                        get = new Invoice();
                        get.InvoiceNo = it.InvoiceNo;
                        get.CustomerNo = it.CustomerNo;
                        get.InvoiceDate = it.InvoiceDate;
                        get.InvoiceAmount = it.InvoiceAmount;
                        get.PaymentDueDate = it.PaymentDueDate;
                        get.ModifyBy = it.ModifyBy;
                        get.ModifyDate = DateTime.Now;
                        Items.Add(get);
                    }

                    Model.Entity.TblInvoices Cust = new Model.Entity.TblInvoices();
                    //Add record

                    Cust = dBContext.TblInvoices.FirstOrDefault(asd => asd.InvoiceNo == InvoiceModel.InvoiceNo);
                    Cust.InvoiceNo = InvoiceModel.InvoiceNo;
                    Cust.CustomerNo = InvoiceModel.CustomerNo;
                    Cust.InvoiceDate = InvoiceModel.InvoiceDate;
                    Cust.InvoiceAmount = InvoiceModel.InvoiceAmount;
                    Cust.PaymentDueDate = Cust.InvoiceDate.AddDays(30);
                    Cust.ModifyBy = InvoiceModel.ModifyBy;
                    Cust.ModifyDate = DateTime.Now;
                    dBContext.TblInvoices.Update(Cust);
                    InvoiceNo = Cust.InvoiceNo;

                    bool Inv = Items.Any(asd => asd.InvoiceNo == InvoiceNo);
                    if (Inv == true)
                    {
                        returnVal = -1;
                    }

                    if (Inv == false)
                    {
                        dBContext.TblInvoices.Add(Cust);
                        returnVal = dBContext.SaveChanges();
                    }

                    returnVal = dBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return returnVal;
        }

        public int DeleteInvoice(string InvoiceNo)
        {
            int returnVal = 0;
            try
            {
                using (var dBContext = new CustomerReportContext())
                {
                    Model.Entity.TblPayment inv = new Model.Entity.TblPayment();
                    Model.Entity.TblInvoices emp = new Model.Entity.TblInvoices();
                    //Add record
                    {
                        
                        emp = dBContext.TblInvoices.FirstOrDefault(asd => asd.InvoiceNo == InvoiceNo);
                        inv = dBContext.TblPayment.FirstOrDefault(asd => asd.InvoiceNo == InvoiceNo);
                        if (inv == null)
                        {
                            if (emp != null)
                            {
                                dBContext.TblInvoices.Remove(emp);
                                returnVal = dBContext.SaveChanges();
                            }
                        }
                        else
                        {
                            returnVal = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return returnVal;
        }

        public List<Invoice> AutoIncrementInvoiceNo()
        {
            List<Invoice> Inv = new List<Invoice>();
            List<Invoice> Inv1 = new List<Invoice>();
            using (var dBContext = new CustomerReportContext())
            {
                Invoice data;
                foreach (var Invoices in dBContext.TblInvoices.ToList())
                {
                    data = new Invoice();
                    data.InvoiceNo = Invoices.InvoiceNo;
                    Inv.Add(data);
                }

                Invoice data2;
                foreach (var auto in dBContext.AutoIncrement.ToList())
                {
                    data2 = new Invoice();
                    int no;
                    no = Convert.ToInt32(auto.AutoInvoiceNo);
                    no += 1;
                    Val = "I" + no.ToString("D5");

                    AutoBack:
                    bool No = Inv.Any(x => x.InvoiceNo == Val);
                    if (No == true)
                    {
                        no += 1;
                        Val = "I" + no.ToString("D5");
                        goto AutoBack;

                    }
                    data2.InvoiceNo = 'I' + Val.Substring(1); ;
                    Inv1.Add(data2);
                }


            }

            return Inv1;

        }

        public Invoice InvoiceById(string InvoiceNo)
        {
            Invoice Cust = new Invoice();
            try
            {
                using (var dBContext = new CustomerReportContext())
                {
                    var dep = dBContext.TblInvoices.Where(x => x.InvoiceNo == InvoiceNo).SingleOrDefault();

                    if (dep != null)
                    {
                        Cust.InvoiceNo = dep.InvoiceNo;
                        Cust.CustomerNo = dep.CustomerNo;
                        Cust.InvoiceDate = dep.InvoiceDate;
                        Cust.InvoiceAmount = dep.InvoiceAmount;
                        Cust.PaymentDueDate = dep.PaymentDueDate;
                    }
                    return Cust;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
