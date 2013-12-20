--creamos campo en subactivity y lo actualizamos
 
USE [FSC_eProject]

alter table subactivity add reponsible  varchar(200) null

update SubActivity set reponsible='no aplica' 
