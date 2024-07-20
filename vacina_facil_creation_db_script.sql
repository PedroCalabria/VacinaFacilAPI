-- create database vacina_facil;

use vacina_facil;

create table dbo.tb_paciente (
 id_paciente int identity,
 dsc_nome varchar(max) not null,
 dat_nascimento datetime not null,
 dsc_email varchar(max) NOT NULL,
 psw_hash varbinary(max) NULL,
 psw_salt varbinary(max) NULL,
 dat_criacao datetime not null,
 constraint PK_TB_PACIENTE primary key (id_paciente)
)
go
create table dbo.tb_agendamento (
 id_agendamento int identity,
 id_paciente int not null,
 dat_agendamento date not null,
 hor_agendamento time not null,
 dsc_status varchar(50) not null,
 dat_criacao datetime not null,
 constraint PK_TB_AGENDAMENTO primary key (id_agendamento)
)
go
alter table dbo.tb_agendamento
add constraint fk_agendamento_paciente
foreign key (id_paciente)
references dbo.tb_paciente (id_paciente)
on delete cascade;
go