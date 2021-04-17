create table Folder (
folderid int identity (1, 1) primary key,
displayname nvarchar(100) not null,
parent_folderid int null
)

go

ALTER TABLE Folder  
ADD CONSTRAINT uniqueFolderName UNIQUE (parent_folderid, displayname);   

go

create table FileExtension(
file_extensionid int identity (1, 1) primary key,
displayname nvarchar(20) not null,
icon_filename nvarchar(200) null
)

go

ALTER TABLE FileExtension  
ADD CONSTRAINT uniqueFileExtensionName UNIQUE (displayname);   

go

create table [File] (
fileid int identity (1, 1) primary key,
displayname nvarchar(200) not null,
description nvarchar(1000) null,
file_extensionid int null,
folderid int null,
file_content varbinary(MAX) null

 FOREIGN KEY (folderid) REFERENCES Folder(folderid)
 on delete cascade,
 FOREIGN KEY (file_extensionid) REFERENCES FileExtension(file_extensionid) 
 on delete set null
)

-- optional
--go

--ALTER TABLE [File] 
--ADD CONSTRAINT uniqueFileName UNIQUE (folderid, displayname);   

go 

insert Folder(displayname, parent_folderid)
values ('автомобили', null),
('гсм', null),
('математика', null)

go

insert Folder(displayname, parent_folderid)
values ('автомобили', 1),
('гсм', 1),
('математика', 1)

go

insert into FileExtension(displayname,icon_filename)
values ('txt','txt'), ('doc','doc'), ('js','js'), ('docx','doc')

go

insert [File](displayname, description, file_extensionid, folderid)
values ('файл_1', '', 1, null)
, ('файл_2', '', 2, 1)
, ('файл_3', '', 1, 2)
, ('файл_4', '', 2, 4)
, ('файл_2', '', 4, 3)