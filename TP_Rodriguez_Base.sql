create database RODRIGUEZJUAN_DB

use RODRIGUEZJUAN_DB

--script para operadores.

create table EstadoOperador
(
Id int identity(1,1),
Descripcion varchar(100),
constraint PK_EstadoOperador primary key (Id)
)
Go

insert into EstadoOperador(Descripcion) values('ACTIVO')--1
insert into EstadoOperador(Descripcion) values('BAJA')--2
insert into EstadoOperador(Descripcion) values('SUSPENDIDO')--3

GO

create table TipoOperador
(
Id int identity(1,1),
Descripcion varchar(100),
constraint PK_TipoOperador primary key (Id)
)
GO

insert into TipoOperador(Descripcion) values('GERENTE')--1
insert into TipoOperador(Descripcion) values('MOZO')--2
insert into TipoOperador(Descripcion) values('COCINA')--3

GO

create table Operador
    (
        Id int identity(1,1),
        Nombre varchar(100) not null,
        Email varchar(100),
        FechaAlta datetime not null,
        EstadoId int not null,
		IdTipoOperador int not null,
		Documento varchar(20) not null,
		Direccion varchar(100) not null,
        Usuario varchar(20)not null,
        Password varchar(20) not null,
		constraint PK_Operador primary key (Id),
		unique(Usuario),
		constraint FK_EstadoId foreign key (EstadoId) references EstadoOperador (Id),
		constraint FK_TipoOperador foreign key (IdTipoOperador) references TipoOperador (Id)
    )
GO

insert into Operador(Nombre, Email, FechaAlta, EstadoId, IdTipoOperador, Documento, Direccion, Usuario, Password) values('GERENTE', 'GERENTE@email.com', getdate(), 1, 1,'123456789', 'Direccion 1111','lvlGerente', 'MTIzNA==')
insert into Operador(Nombre, Email, FechaAlta, EstadoId, IdTipoOperador, Documento, Direccion, Usuario, Password) values('MOZO', 'MOZO@email.com', getdate(), 1, 2,'123456789', 'Direccion 2222', 'lvlMozo', 'MTIzNA==')
insert into Operador(Nombre, Email, FechaAlta, EstadoId, IdTipoOperador, Documento, Direccion, Usuario, Password) values('COCINA', 'COCINA@email.com', getdate(), 1, 3,'123456789','Direccion 3333', 'lvlCocina', 'MTIzNA==' )

GO

create procedure SP_GetOperadorByUserPass
(
@usuario varchar(20),
@password varchar(20)
)
as
begin
select Id, Nombre, Email, FechaAlta, EstadoId, IdTipoOperador, Documento, Direccion, Usuario from Operador where Usuario = @usuario and Password = @password
end

GO

create procedure [dbo].[SP_GetAllOperadores]
as
begin
select o.Id, o.Nombre, o.Email, e.Descripcion, t.Descripcion, o.Documento, o.Direccion, o.Usuario from Operador o
inner join EstadoOperador e on o.EstadoId = e.Id
inner join TipoOperador t on o.IdTipoOperador = t.Id
order by 1 asc
end

GO

create procedure SP_GetAllTipoOperador
as
begin
select * from TipoOperador
order by 1 asc
end

GO

create procedure SP_GetAllEstadoOperador
as
begin
select * from EstadoOperador
order by 1 asc
end

GO

create procedure SP_CheckOperadoreExist
@Usuario varchar(20)
as
begin
select * from Operador
where Usuario = @Usuario
end

GO

create procedure SP_CreateNewOperador
(
@Nombre varchar(100),
@Email varchar(100),
@EstadoId int,
@IdTipoOperador int,
@Documento varchar(20),
@Direccion varchar(100),
@Usuario varchar(20),
@Password varchar(20)
)
as
begin
insert into Operador(Nombre, Email, FechaAlta, EstadoId, IdTipoOperador, Documento, Direccion, Usuario, Password) 
values(@Nombre, @Email, getdate(), @EstadoId, @IdTipoOperador, @Documento, @Direccion, @Usuario, @Password)
end

go

create table MesaEstado
(
Id int identity(1,1),
Descripcion varchar(20),
constraint PK_MESA_ESTADO primary key (Id)
)

go

insert into MesaEstado (Descripcion) Values('OCUPADA')
insert into MesaEstado (Descripcion) Values('LIBRE')

GO

create table Mesa
(
Id int identity(1,1),
NroMesa int unique,
EstadoId int not null,
Asignada int not null,
constraint PK_MESA primary key (Id),
constraint FK_MESA_ESTADO foreign key (EstadoId) references MesaEstado (Id)
)

GO

insert into Mesa (NroMesa, EstadoId, Asignada) values(1,2,0)
insert into Mesa (NroMesa, EstadoId, Asignada) values(2,2,0)
insert into Mesa (NroMesa, EstadoId, Asignada) values(3,2,0)

Go

create table MesaPorOperador
(
Id int identity(1,1),
IdOperador int not null,
IdMesa int not null,
constraint PK_MesaPorOperador primary key (Id),
constraint FK_MESA_Operador foreign key (IdOperador) references Operador (Id),
constraint FK_MESA_MESA foreign key (IdMesa) references Mesa (Id)
)

go

insert into MesaPorOperador (IdOperador, IdMesa) values (1,1)
insert into MesaPorOperador (IdOperador, IdMesa) values (1,2)

go

create procedure MesasByOperador
(
@IdOperador int
)
as
begin
select * from MesaPorOperador where IdOperador =  @IdOperador
end

go

create table EstadoComanda
(
Id int identity(1,1),
Descripcion varchar(30) not null,
constraint PK_EstadoComanda primary key (Id)
)

go

insert into EstadoComanda (Descripcion) values('Libre')
insert into EstadoComanda (Descripcion) values('Tomada')
insert into EstadoComanda (Descripcion) values('Finalizada')

go

create table Producto
(
Id int identity(1,1),
Precio money not null,
Stock int not null,
Descripcion varchar(200) not null,
Sku varchar(20) unique,
constraint PK_Producto primary key(Id)
)

go

insert into Producto(Precio, Stock, Descripcion, Sku) values(100, 10, 'Hamburguesa', '123456')
insert into Producto(Precio, Stock, Descripcion, Sku) values(60, 10, 'Gaseosa', '645891')
insert into Producto(Precio, Stock, Descripcion, Sku) values(90, 10, 'Papas Pack', '157415')
insert into Producto(Precio, Stock, Descripcion, Sku) values(120, 10, 'Bife', '669966')

go

--create table Menu
--(
--Id int identity(1,1),
--IdProductos varchar(100),
--DescripcionMenu varchar(200),
--constraint PK_Menu primary key(Id)
--)

--go

--insert into Menu(IdProductos, DescripcionMenu) values('1|2|3', 'Menu Infantil')
--insert into Menu(IdProductos, DescripcionMenu) values('2|3|4', 'Menu Adulto')

create table Comanda
(
Id bigint identity(1,1),
Menu varchar(1000) not null,
FechaInicio datetime not null,
FechaFin datetime null,
IdEstado int not null,
IdOperador int not null,
constraint PK_Comanda primary key (Id),
constraint FK_Comanda_EstadoComanda foreign key (IdEstado) references EstadoComanda (Id),
constraint FK_Comanda_Operador foreign key (IdOperador) references Operador (Id),
)

go

insert into Comanda (Menu,FechaInicio,FechaFin, IdEstado, IdOperador) values('pizza x 2', GETDATE(), null, 1, 2)
insert into Comanda (Menu,FechaInicio,FechaFin, IdEstado, IdOperador) values('fideos x 3', GETDATE(), null, 1, 2)

go

Create table ComandaPorOperador
(
Id int identity(1,1),
IdOperador int not null,
IdComanda bigint not null,
constraint PK_ComandaPorOperador primary key (Id),
constraint FK_ComandaPorOperador_Operador foreign key (IdOperador) references Operador (Id),
constraint FK_ComandaPorOperador_Comanda foreign key (IdComanda) references Comanda (Id)
)

go

insert into ComandaPorOperador(IdComanda, IdOperador) values(1,3)
insert into ComandaPorOperador(IdComanda, IdOperador) values(2,3)

go

create procedure SP_GetComandasPorOperador
(
@IdOperador int,
@IdEstado int
)
as
begin
select * from Comanda where IdOperador = @IdOperador and IdEstado = @IdEstado
end

go

create procedure SP_CrearProducto
(
@Descripcion varchar(200),
@Sku varchar(20),
@Stock int,
@Precio money
)
as
begin
insert into Producto(Precio, Stock, Descripcion, Sku) values(@Precio, @Stock, @Descripcion, @Sku)
end

go

create procedure SP_AltaMesa
(
@NroMesa int,
@EstadoId int,
@Asignada int
)
as
begin
insert into Mesa (NroMesa, EstadoId, Asignada) values(@NroMesa, @EstadoId, @Asignada)
end

GO

create procedure SP_ExisteNroMesa
(
@NroMesa int
)
as
begin
select * from Mesa
where NroMesa = @NroMesa
end

GO

create procedure [dbo].[SP_GetOperadorById]
(
@id int
)
as
begin
select Id, Nombre, Email, EstadoId, IdTipoOperador, Documento, Direccion, Usuario, Password from Operador where Id = @id
end

GO

create procedure SP_ModificarOperador
(
@Id int,
@Nombre varchar(100),
@Email varchar(100),
@EstadoId int,
@IdTipoOperador int,
@Documento varchar(20),
@Direccion varchar(100),
@Usuario varchar(20),
@Password varchar(20)
)
as
begin
update Operador set Nombre = @Nombre, Email = @Email, EstadoId = @EstadoId, IdTipoOperador = @IdTipoOperador, Documento = @Documento, Direccion = @Direccion, Usuario = @Usuario, Password = @Password 
where Id = @Id
end

Go

create procedure [dbo].[SP_EliminarMesa]
(
@Id int
)
as
begin
delete from MesaPorOperador where IdMesa = @Id
update Mesa set Asignada = 0, EstadoId = 2 where Id = @Id
end

Go

create procedure [dbo].[SP_GetAllMesas]
as
begin
select mp.Id as IdMp, m.Id as MesaId,o.Id as IdOperador, m.NroMesa, o.Usuario from Mesa m
left join MesaPorOperador mp on m.Id = mp.IdMesa
left join Operador o on mp.IdOperador = o.Id
end

GO

CREATE procedure [dbo].[SP_ModificarProducto]
(
@Id int,
@Descripcion varchar(200),
@Sku varchar(20),
@Stock int,
@Precio money
)
as
begin
update Producto set Precio = @Precio, Stock = @Stock, Descripcion = @Descripcion, Sku = @Sku where Id = @Id
end

GO

create procedure [dbo].[SP_GetProductoById]
(
@id int
)
as
begin
select Id, Precio, Stock, Descripcion, Sku from Producto where Id = @id
end

go

create procedure [dbo].[SP_GetAllProductos]
as
begin
select p.Id, p.Precio, p.Stock, p.Descripcion, p.Sku from Producto p
order by p.Id asc
end

go

create procedure [dbo].[SP_GetMesaById]
(
@Id int
)
as
begin
select * from Mesa
where Id = @Id
end
go

create procedure [dbo].[SP_GetAllOperadoresByDocument]
(
@Documento varchar(20)
)
as
begin
select o.Id, o.Nombre, o.Email, e.Descripcion, t.Descripcion, o.Documento, o.Direccion, o.Usuario from Operador o
inner join EstadoOperador e on o.EstadoId = e.Id
inner join TipoOperador t on o.IdTipoOperador = t.Id
where o.Documento like '%' + @Documento + '%'
end
go

create procedure [dbo].[SP_GetAllOperadoresByName]
(
@Nombre varchar(100)
)
as
begin
select o.Id, o.Nombre, o.Email, e.Descripcion, t.Descripcion, o.Documento, o.Direccion, o.Usuario from Operador o
inner join EstadoOperador e on o.EstadoId = e.Id
inner join TipoOperador t on o.IdTipoOperador = t.Id
where o.Nombre like '%' + @Nombre + '%'
end

go

create procedure [dbo].[SP_AsignarMesa]
(
@IdMesa int,
@IdOperador int,
@IdMesaOperador int
)
as
begin
update Mesa set Asignada = 1 where Id = @IdMesa
update MesaPorOperador set IdMesa = @IdMesa, IdOperador = @IdOperador where Id = @IdMesaOperador
end

go

create procedure [dbo].[SP_CreateAsignarMesa]
(
@IdMesa int,
@IdOperador int,
@IdMesaOperador int = 0
)
as
begin
insert into MesaPorOperador(IdMesa, IdOperador) values(@IdMesa, @IdOperador)
update Mesa set Asignada = 1 where Id = @IdMesa
end

go

create procedure [dbo].[Sp_GetMesasOpUnassign]
(
@IdOperador int
)
as
begin
select m.Id, m.NroMesa from Mesa m
inner join MesaPorOperador op on op.IdMesa = m.Id and op.IdOperador = @IdOperador
where m.EstadoId = 2
end
go

create procedure [dbo].[Sp_OcuparMesa]
(
@IdMesa int
)
as
begin
update Mesa set  EstadoId = 1 where id = @IdMesa
end

go

create procedure [dbo].[SP_GetAllMesasAsigOperador]
(
@IdOperador int
)
as
begin
select mp.Id as IdMp, m.Id as MesaId,o.Id as IdOperador, m.NroMesa, o.Usuario  from Mesa m
left join MesaPorOperador mp on m.Id = mp.IdMesa and mp.IdOperador = @IdOperador
left outer join Operador o on o.Id = mp.IdOperador
where m.EstadoId = 1
end
go

create procedure [dbo].[Sp_CerrarMesa]
(
@IdMesa int
)
as
begin
update Mesa set  EstadoId = 2 where id = @IdMesa
end

go

create table TempPedidoPorMesa
(
Id bigint identity(1,1),
IdMesa int not null,
IdProducto int not null,
constraint PK_TempPedidoPorMesa primary key (Id),
constraint FK_TempPedidoPorMesa_Producto foreign key (IdProducto) references Producto (Id),
constraint FK_TempPedidoPorMesa_Mesa foreign key (IdMesa) references Mesa (Id)
)

go

create procedure [dbo].[SP_InsertPedidosMesa]
(
@IdMesa int,
@IdProducto int
)
as
begin
insert into TempPedidoPorMesa(IdMesa, IdProducto) values(@IdMesa, @IdProducto)
end

go

create procedure [dbo].[SP_GetAllPedidosMesa]
(
@IdMesa int
)
as
begin
select t.IdProducto, count(t.IdProducto), p.Descripcion, p.Precio, p.Stock from TempPedidoPorMesa t
inner join Producto p on p.Id = t.IdProducto
where t.IdMesa = @IdMesa
group by t.IdProducto, p.Descripcion,p.Precio, p.Stock
end

go

create procedure SP_CloseAllPedidosMesa
(
@IdMesa int
)
as
begin
delete from TempPedidoPorMesa where IdMesa = @IdMesa
end

go

create procedure SP_RemovePedidosMesa
(
@IdMesa int,
@IdProducto int
)
as
begin
delete from TempPedidoPorMesa where Id = (select top 1 Id from TempPedidoPorMesa t where t.IdMesa = @IdMesa and t.IdProducto = @IdProducto)
end
go

create procedure SP_InsertComanda
(
@Menu varchar(1000),
@FechaInicio datetime,
@IdEstado int,
@IdOperador int
)
as
begin
insert into Comanda (Menu,FechaInicio,FechaFin, IdEstado, IdOperador) values(@Menu, @FechaInicio, null, @IdEstado, @IdOperador)
end
go

create procedure SP_InsertComandaOperador
(
@IdOperador int,
@IdComanda int
)
as
begin
insert into ComandaPorOperador (IdOperador,IdComanda) values(@IdOperador, @IdComanda)
end
go

create procedure SP_DescontarCantidad
(
@Cantidad int,
@IdProducto int
)
as
begin
update Producto set Stock = @Cantidad where Id = @IdProducto
end
go

create table Ticket
(
Id bigint not null identity(1,1),
IdOperador int not null,
IdMesa int not null,
NroMesa int not null,
Operador varchar(100) not null,
Total money not null,
Fecha datetime not null,
Descripcion varchar(1000),
constraint PK_Ticket primary key (Id)
)
go

create procedure [dbo].[InsertTicket]
(
@IdMesa int,
@IdOperador int,
@NroMesa int,
@Operador varchar(100),
@Total money,
@Fecha datetime,
@Desc varchar(100)
)
as
begin
insert into Ticket(IdOperador,IdMesa,Operador,NroMesa, Total, Fecha, Descripcion) values(@IdOperador,@IdMesa,@Operador,@NroMesa,@Total,@Fecha, @Desc)
end
go

create procedure [dbo].[SP_GetComandasTipoUno]
as
begin
select * from Comanda where IdEstado = (select e.Id from EstadoComanda e where Descripcion = 'Espera')
end
go

create procedure [dbo].[SP_GetComandasTipoDos]
as
begin
select * from Comanda where IdEstado = (select e.Id from EstadoComanda e where Descripcion = 'Tomada')
end
go

create procedure [dbo].[SP_CambiarEstadoComanda]
(
@Descripcion varchar(20),
@Id bigint
)
as
begin
update Comanda set IdEstado = (select e.Id from EstadoComanda e where Descripcion = @Descripcion), FechaFin = GETDATE() where Id = @Id
end

GO

create procedure SP_Top_Ventas
(
@FechaDesde datetime,
@FechaHasta datetime
)
as
begin
select Operador, sum(Total) as Ventas, cast(Fecha as date) as Fedate from Ticket
where cast(Fecha as date) between cast(@FechaDesde as date) and cast(@FechaHasta as date)
group by Operador, cast(Fecha as date)
order by Ventas
end

GO

create procedure SP_Comandas_Ventas
(
@FechaDesde datetime,
@FechaHasta datetime
)
as
begin
select Menu, cast(FechaFin as date) as Fedate, op.Nombre from Comanda
inner join Operador op on Comanda.IdOperador = op.Id
where cast(FechaFin as date) between cast(@FechaDesde as date) and cast(@FechaHasta as date)
order by Menu, op.Nombre
end

