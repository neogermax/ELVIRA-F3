-- SCRIPT DE ACTUALIZACION DE PROYECTOS ANTIGUOS

UPDATE Project SET Mother=1 ,Project_derivados=ID WHERE Mother IS NULL