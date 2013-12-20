use FSC_eSecurity
  delete from MenusByUserGroup where idusergroup = 1 and idmenu = 197
  delete from MenusByUserGroup where IdUserGroup = 1 and IdMenu = 228
  delete from MenusByUserGroup where IdUserGroup = 1 and IdMenu = 227
  insert into MenusByUserGroup (IdUserGroup,IdMenu) values(35,336)
  insert into MenusByUserGroup (IdUserGroup,IdMenu) values(35,340)
  insert into MenusByUserGroup (IdUserGroup,IdMenu) values(35,343)
  insert into MenusByUserGroup (IdUserGroup,IdMenu) values(35,346)
  update Menu set TextField = 'Listado de actividades pendientes' where URL = ' /FSC/Execution/addSubActivityInformationRegistry.aspx?op=add&idsubactivity=2593'