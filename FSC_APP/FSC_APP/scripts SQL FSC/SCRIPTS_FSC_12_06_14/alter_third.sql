--campos alter campo persona natural
ALTER TABLE third alter column PersonaNatural int null;
ALTER TABLE third alter column direccion varchar(500) null;
--reversa
ALTER TABLE third alter column direccion varchar(10) null;
ALTER TABLE third alter column PersonaNatural bit null;
