/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     10/22/2011 10:31:11 AM                       */
/*==============================================================*/

create Database TravelH2V
go

use TravelH2V
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('places') and o.name = 'FK_PLACES_REFERENCE_PLACE_CA')
alter table places
   drop constraint FK_PLACES_REFERENCE_PLACE_CA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('real_ratings') and o.name = 'FK_REAL_RAT_REFERENCE_MOOD')
alter table real_ratings
   drop constraint FK_REAL_RAT_REFERENCE_MOOD
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('real_ratings') and o.name = 'FK_REAL_RAT_REFERENCE_BUDGET')
alter table real_ratings
   drop constraint FK_REAL_RAT_REFERENCE_BUDGET
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('real_ratings') and o.name = 'FK_REAL_RAT_REFERENCE_FAMILIAR')
alter table real_ratings
   drop constraint FK_REAL_RAT_REFERENCE_FAMILIAR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('real_ratings') and o.name = 'FK_REAL_RAT_REFERENCE_USERS')
alter table real_ratings
   drop constraint FK_REAL_RAT_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('real_ratings') and o.name = 'FK_REAL_RAT_REFERENCE_PLACES')
alter table real_ratings
   drop constraint FK_REAL_RAT_REFERENCE_PLACES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('real_ratings') and o.name = 'FK_REAL_RAT_REFERENCE_WEATHER')
alter table real_ratings
   drop constraint FK_REAL_RAT_REFERENCE_WEATHER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('real_ratings') and o.name = 'FK_REAL_RAT_REFERENCE_TEMPERAT')
alter table real_ratings
   drop constraint FK_REAL_RAT_REFERENCE_TEMPERAT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('real_ratings') and o.name = 'FK_REAL_RAT_REFERENCE_TRAVEL_L')
alter table real_ratings
   drop constraint FK_REAL_RAT_REFERENCE_TRAVEL_L
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('real_ratings') and o.name = 'FK_REAL_RAT_REFERENCE_COMPANIO')
alter table real_ratings
   drop constraint FK_REAL_RAT_REFERENCE_COMPANIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('budget')
            and   type = 'U')
   drop table budget
go

if exists (select 1
            from  sysobjects
           where  id = object_id('companion')
            and   type = 'U')
   drop table companion
go

if exists (select 1
            from  sysobjects
           where  id = object_id('familiarity')
            and   type = 'U')
   drop table familiarity
go

if exists (select 1
            from  sysobjects
           where  id = object_id('mood')
            and   type = 'U')
   drop table mood
go

if exists (select 1
            from  sysobjects
           where  id = object_id('place_categories')
            and   type = 'U')
   drop table place_categories
go

if exists (select 1
            from  sysobjects
           where  id = object_id('places')
            and   type = 'U')
   drop table places
go

if exists (select 1
            from  sysobjects
           where  id = object_id('real_ratings')
            and   type = 'U')
   drop table real_ratings
go

if exists (select 1
            from  sysobjects
           where  id = object_id('temperature')
            and   type = 'U')
   drop table temperature
go

if exists (select 1
            from  sysobjects
           where  id = object_id('travel_length')
            and   type = 'U')
   drop table travel_length
go

if exists (select 1
            from  sysobjects
           where  id = object_id('users')
            and   type = 'U')
   drop table users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('weather')
            and   type = 'U')
   drop table weather
go

/*==============================================================*/
/* Table: budget                                                */
/*==============================================================*/
create table budget (
   id                   int   identity               not null,
   budget               nvarchar(50)         null,
   constraint PK_BUDGET primary key (id)
)
go

/*==============================================================*/
/* Table: companion                                             */
/*==============================================================*/
create table companion (
   id                   int    identity              not null,
   companion            nvarchar(50)         null,
   constraint PK_COMPANION primary key (id)
)
go

/*==============================================================*/
/* Table: familiarity                                           */
/*==============================================================*/
create table familiarity (
   id                   int     identity             not null,
   familiarity          nvarchar(50)         null,
   constraint PK_FAMILIARITY primary key (id)
)
go

/*==============================================================*/
/* Table: mood                                                  */
/*==============================================================*/
create table mood (
   id                   int    identity              not null,
   mood                 nvarchar(50)         null,
   constraint PK_MOOD primary key (id)
)
go

/*==============================================================*/
/* Table: place_categories                                      */
/*==============================================================*/
create table place_categories (
   id                   int      identity            not null,
   place_category       nvarchar(100)        null,
   constraint PK_PLACE_CATEGORIES primary key (id)
)
go

/*==============================================================*/
/* Table: places                                                */
/*==============================================================*/
create table places (
   id                   int      identity            not null,
   id_place_category    int                  null,
   name                 nvarchar(500)        null,
   imgurl               nvarchar(1000)       null,
   lat                  float                null,
   lng                  float                null,
   house_number         nvarchar(50)         null,
   street               nvarchar(255)        null,
   ward                 nvarchar(100)        null,
   district             nvarchar(100)        null,
   city                 nvarchar(100)        null,
   province             nvarchar(100)        null,
   country              nvarchar(100)        null,
   phone_number         nvarchar(100)        null,
   email                nvarchar(100)        null,
   website              nvarchar(100)        null,
   history              ntext                null,
   details              ntext                null,
   sources              ntext                null,
   general_rating       float                null,
   general_count_rating float                null,
   general_sum_rating   float                null,
   constraint PK_PLACES primary key (id)
)
go

/*==============================================================*/
/* Table: real_ratings                                          */
/*==============================================================*/
create table real_ratings (
   id                   int      identity            not null,
   id_user              int                  null,
   id_place             int                  null,
   id_temperature       int                  null,
   id_companion         int                  null,
   id_farmiliarity      int                  null,
   id_mood              int                  null,
   id_budget            int                  null,
   id_weather           int                  null,
   id_travel_length     int                  null,
   time                 datetime             null,
   rating               float                null,
   constraint PK_REAL_RATINGS primary key (id)
)
go

/*==============================================================*/
/* Table: temperature                                           */
/*==============================================================*/
create table temperature (
   id                   int   identity               not null,
   temperature          nvarchar(50)         null,
   constraint PK_TEMPERATURE primary key (id)
)
go

/*==============================================================*/
/* Table: travel_length                                         */
/*==============================================================*/
create table travel_length (
   id                   int    identity              not null,
   travel_length        nvarchar(50)         null,
   constraint PK_TRAVEL_LENGTH primary key (id)
)
go

/*==============================================================*/
/* Table: users                                                 */
/*==============================================================*/
create table users (
   id                   int    identity(10000,1)              not null,
   email                nvarchar(100)        null,
   birthday				datetime             null,
   gender               tinyint              null,
   constraint PK_USERS primary key (id)
)
go

/*==============================================================*/
/* Table: weather                                               */
/*==============================================================*/
create table weather (
   id                   int     identity             not null,
   weather              nvarchar(50)         null,
   constraint PK_WEATHER primary key (id)
)
go

alter table places
   add constraint FK_PLACES_REFERENCE_PLACE_CA foreign key (id_place_category)
      references place_categories (id) on delete cascade
go

alter table real_ratings
   add constraint FK_REAL_RAT_REFERENCE_MOOD foreign key (id_mood)
      references mood (id) on delete cascade
go

alter table real_ratings
   add constraint FK_REAL_RAT_REFERENCE_BUDGET foreign key (id_budget)
      references budget (id) on delete cascade
go

alter table real_ratings
   add constraint FK_REAL_RAT_REFERENCE_FAMILIAR foreign key (id_farmiliarity)
      references familiarity (id) on delete cascade
go

alter table real_ratings
   add constraint FK_REAL_RAT_REFERENCE_USERS foreign key (id_user)
      references users (id) on delete cascade
go

alter table real_ratings
   add constraint FK_REAL_RAT_REFERENCE_PLACES foreign key (id_place)
      references places (id) on delete cascade
go

alter table real_ratings
   add constraint FK_REAL_RAT_REFERENCE_WEATHER foreign key (id_weather)
      references weather (id) on delete cascade
go

alter table real_ratings
   add constraint FK_REAL_RAT_REFERENCE_TEMPERAT foreign key (id_temperature)
      references temperature (id) on delete cascade
go

alter table real_ratings
   add constraint FK_REAL_RAT_REFERENCE_TRAVEL_L foreign key (id_travel_length)
      references travel_length (id) on delete cascade
go

alter table real_ratings
   add constraint FK_REAL_RAT_REFERENCE_COMPANIO foreign key (id_companion)
      references companion (id) on delete cascade
go
