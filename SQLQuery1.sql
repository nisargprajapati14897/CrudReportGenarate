﻿CREATE TABLE tblCustomer (
    [Customer_No] NVARCHAR(10),
    [Customer_Name] NVARCHAR(50)
);
INSERT INTO tblCustomer VALUES
    (N'C00001',N'Kamal Enterprise'),
    (N'C00002',N'Bharit Shah & Co.'),
    (N'C00003',N'Vishal Doshi'),
    (N'C00004',N'Khushi Keral Pvt. Ltd'),
    (N'C00005',N'Jiten Gandhi & Brothers'),
    (N'C00006',N'Bhumi Associates');


 CREATE TABLE tblInvoices (
    [Invoice_No] NVARCHAR(10),
    [Customer_No] NVARCHAR(50),
    [Invoice_Date] DATETIME,
    [Invoice_Amount] INT,
    [Payment_Due_Date] DATETIME
);
INSERT INTO tblInvoices VALUES
    (N'I00001',N'C00001','2019-01-11 00:00:00',8700,'2019-02-10 00:00:00'),
    (N'I00002',N'C00001','2019-01-19 00:00:00',9200,'2019-02-18 00:00:00'),
    (N'I00003',N'C00002','2019-01-22 00:00:00',11690,'2019-02-21 00:00:00'),
    (N'I00004',N'C00003','2019-01-22 00:00:00',13000,'2019-02-21 00:00:00'),
    (N'I00005',N'C00001','2019-01-30 00:00:00',14360,'2019-03-01 00:00:00'),
    (N'I00006',N'C00004','2019-02-16 00:00:00',6540,'2019-03-18 00:00:00'),
    (N'I00007',N'C00005','2019-02-19 00:00:00',22100,'2019-03-21 00:00:00'),
    (N'I00008',N'C00003','2019-02-27 00:00:00',1490,'2019-03-29 00:00:00'),
    (N'I00009',N'C00003','2019-03-01 00:00:00',16400,'2019-03-31 00:00:00'),
    (N'I00010',N'C00002','2019-03-11 00:00:00',4500,'2019-04-10 00:00:00'),
    (N'I00011',N'C00006','2019-03-16 00:00:00',3400,'2019-04-15 00:00:00'),
    (N'I00012',N'C00004','2019-03-29 00:00:00',9800,'2019-04-28 00:00:00'),
    (N'I00013',N'C00001','2019-04-04 00:00:00',13800,'2019-05-04 00:00:00'),
    (N'I00014',N'C00002','2019-04-18 00:00:00',10190,'2019-05-18 00:00:00'),
    (N'I00015',N'C00002','2019-04-26 00:00:00',9000,'2019-05-26 00:00:00'),
    (N'I00016',N'C00005','2019-04-30 00:00:00',6800,'2019-05-30 00:00:00'),
    (N'I00017',N'C00002','2019-05-01 00:00:00',14360,'2019-05-31 00:00:00'),
    (N'I00018',N'C00006','2019-05-12 00:00:00',8750,'2019-06-11 00:00:00'),
    (N'I00019',N'C00003','2019-05-12 00:00:00',22100,'2019-06-11 00:00:00'),
    (N'I00020',N'C00002','2019-05-17 00:00:00',4500,'2019-06-16 00:00:00');

CREATE TABLE tblPayment (
    [Payment_No] NVARCHAR(10),
    [Invoice_No] NVARCHAR(10),
    [Payment_Date] DATETIME,
    [Payment_Amount] INT
);
INSERT INTO tblPayment VALUES
    (N'P00001',N'I00001','2019-01-16 00:00:00',3200),
    (N'P00002',N'I00002','2019-01-22 00:00:00',9200),
    (N'P00003',N'I00001','2019-01-27 00:00:00',5600),
    (N'P00004',N'I00003','2019-02-19 00:00:00',9000),
    (N'P00005',N'I00005','2019-02-24 00:00:00',14360),
    (N'P00006',N'I00004','2019-02-28 00:00:00',13000),
    (N'P00007',N'I00003','2019-03-01 00:00:00',2000),
    (N'P00008',N'I00006','2019-03-18 00:00:00',6540),
    (N'P00009',N'I00008','2019-03-22 00:00:00',1490),
    (N'P00010',N'I00011','2019-03-29 00:00:00',3400),
    (N'P00011',N'I00009','2019-03-31 00:00:00',11000),
    (N'P00012',N'I00010','2019-04-04 00:00:00',4500),
    (N'P00013',N'I00013','2019-04-16 00:00:00',13800),
    (N'P00014',N'I00014','2019-04-30 00:00:00',10190),
    (N'P00015',N'I00016','2019-05-04 00:00:00',6800),
    (N'P00016',N'I00017','2019-05-10 00:00:00',10000),
    (N'P00017',N'I00015','2020-05-19 00:00:00',9000),
    (N'P00018',N'I00017','2019-05-27 00:00:00',4000),
    (N'P00019',N'I00019','2019-05-31 00:00:00',20000),
    (N'P00020',N'I00020','2019-05-31 00:00:00',4500),
    (N'P00021',N'I00017','2019-05-31 00:00:00',360);


select * from tblCustomer;

select * from  tblInvoices;

select * from  tblPayment;
