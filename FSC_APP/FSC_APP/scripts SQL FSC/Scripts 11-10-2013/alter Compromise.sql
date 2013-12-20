-- campos para compromiso solicitados para logs de finalizacion
use FSC_eProject

ALTER TABLE compromise ADD Enddate Datetime null
ALTER TABLE compromise ADD IdUser  varchar(50) null
ALTER TABLE compromise ADD DateMod Datetime null




