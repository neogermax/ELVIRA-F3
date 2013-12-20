USE [FSC_eProject]
update Third set PersonaNatural='True' where ID IN(2,22)

USE [FSC_eProject]
update Third set PersonaNatural='False' where PersonaNatural is null
