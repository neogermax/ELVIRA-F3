use FSC_eSecurity
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,193)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,384)
update Menu set level = 3 where id = 375
update Menu set level = 4 where id = 376
update Menu set level = 4 where id = 377
update Menu set level = 3 where id = 208
delete from MenusByUserGroup where idusergroup = 1 and idmenu= 208