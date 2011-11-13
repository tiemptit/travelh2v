/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     11/10/2011 10:18:21 PM                       */
/*==============================================================*/
create Database TravelH2V
go

use TravelH2V
go


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_MOOD')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_MOOD
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_BUDG')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_BUDG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_FAMI')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_FAMI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_DATE')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_DATE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_USER')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_PLAC')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_PLAC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_WEAT')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_WEAT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_TEMP')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_TEMP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_TRAV')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_TRAV
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('fact_ratings') and o.name = 'FK_FACT_RAT_REFERENCE_DIM_COMP')
alter table fact_ratings
   drop constraint FK_FACT_RAT_REFERENCE_DIM_COMP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_budget')
            and   type = 'U')
   drop table dim_budget
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_companion')
            and   type = 'U')
   drop table dim_companion
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_date')
            and   type = 'U')
   drop table dim_date
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_familiarity')
            and   type = 'U')
   drop table dim_familiarity
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_mood')
            and   type = 'U')
   drop table dim_mood
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_place')
            and   type = 'U')
   drop table dim_place
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_temperature')
            and   type = 'U')
   drop table dim_temperature
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_travel_length')
            and   type = 'U')
   drop table dim_travel_length
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_user')
            and   type = 'U')
   drop table dim_user
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dim_weather')
            and   type = 'U')
   drop table dim_weather
go

if exists (select 1
            from  sysobjects
           where  id = object_id('fact_ratings')
            and   type = 'U')
   drop table fact_ratings
go

/*==============================================================*/
/* Table: dim_budget                                            */
/*==============================================================*/
create table dim_budget (
   budget_key           int                  not null,
   budget               nvarchar(50)         null,
   constraint PK_DIM_BUDGET primary key (budget_key)
)
go

/*==============================================================*/
/* Table: dim_companion                                         */
/*==============================================================*/
create table dim_companion (
   companion_key        int                  not null,
   companion            nvarchar(50)         null,
   constraint PK_DIM_COMPANION primary key (companion_key)
)
go

/*==============================================================*/
/* Table: dim_date                                              */
/*==============================================================*/
create table dim_date (
   date_key             int                  not null,
   period_of_day        nvarchar(50)         null,
   period_of_week       nvarchar(50)         null,
   season               nvarchar(50)         null,
   constraint PK_DIM_DATE primary key (date_key)
)
go

/*==============================================================*/
/* Table: dim_familiarity                                       */
/*==============================================================*/
create table dim_familiarity (
   familiarity_key      int                  not null,
   familiarity          nvarchar(50)         null,
   constraint PK_DIM_FAMILIARITY primary key (familiarity_key)
)
go

/*==============================================================*/
/* Table: dim_mood                                              */
/*==============================================================*/
create table dim_mood (
   mood_key             int                  not null,
   mood                 nvarchar(50)         null,
   constraint PK_DIM_MOOD primary key (mood_key)
)
go

/*==============================================================*/
/* Table: dim_place                                             */
/*==============================================================*/
create table dim_place (
   place_key            int                  not null,
   place_category       nvarchar(100)        null,
   name                 nvarchar(500)        null,
   imgurl               nvarchar(1000)       null,
   lat                  float                null,
   lng                  float                null,
   house_number         nvarchar(15)         null,
   street               nvarchar(100)        null,
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
   general_sum_rating   float                null,
   general_count_rating float                null,
   constraint PK_DIM_PLACE primary key (place_key)
)
go

/*==============================================================*/
/* Table: dim_temperature                                       */
/*==============================================================*/
create table dim_temperature (
   temperature_key      int                  not null,
   temperature          nvarchar(50)         null,
   constraint PK_DIM_TEMPERATURE primary key (temperature_key)
)
go

/*==============================================================*/
/* Table: dim_travel_length                                     */
/*==============================================================*/
create table dim_travel_length (
   travellength_key     int                  not null,
   travel_length        nvarchar(50)         null,
   constraint PK_DIM_TRAVEL_LENGTH primary key (travellength_key)
)
go

/*==============================================================*/
/* Table: dim_user                                              */
/*==============================================================*/
create table dim_user (
   user_key             int                  not null,
   email                nvarchar(100)        null,
   password             nvarchar(100)        null,
   year_of_birth        int                  null,
   gender               tinyint              null,
   constraint PK_DIM_USER primary key (user_key)
)
go

/*==============================================================*/
/* Table: dim_weather                                           */
/*==============================================================*/
create table dim_weather (
   weather_key          int                  not null,
   weather              nvarchar(50)         null,
   constraint PK_DIM_WEATHER primary key (weather_key)
)
go

/*==============================================================*/
/* Table: fact_ratings                                          */
/*==============================================================*/
create table fact_ratings (
   user_key             int                  not null,
   place_key            int                  null,
   weather_key          int                  null,
   temperature_key      int                  null,
   travelleng_key       int                  null,
   companion_key        int                  null,
   familiarity_key      int                  null,
   mood_key             int                  null,
   budget_key           int                  null,
   date_key             int                  null,
   rating               float                null
)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_MOOD foreign key (mood_key)
      references dim_mood (mood_key)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_BUDG foreign key (budget_key)
      references dim_budget (budget_key)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_FAMI foreign key (familiarity_key)
      references dim_familiarity (familiarity_key)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_DATE foreign key (date_key)
      references dim_date (date_key)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_USER foreign key (user_key)
      references dim_user (user_key)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_PLAC foreign key (place_key)
      references dim_place (place_key)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_WEAT foreign key (weather_key)
      references dim_weather (weather_key)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_TEMP foreign key (temperature_key)
      references dim_temperature (temperature_key)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_TRAV foreign key (travelleng_key)
      references dim_travel_length (travellength_key)
go

alter table fact_ratings
   add constraint FK_FACT_RAT_REFERENCE_DIM_COMP foreign key (companion_key)
      references dim_companion (companion_key)
go

