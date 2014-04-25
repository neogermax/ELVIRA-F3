-- CAMBIO DE CAMPOS DE FLUJOS DE PAGOS SI DECIMALES
USE [FSC_eProject]

ALTER TABLE Paymentflow alter column valorparcial numeric(18,0) null;
ALTER TABLE Paymentflow alter column valortotal numeric(18,0) null;
ALTER TABLE Paymentflow alter column porcentaje varchar(5) null;