use TravelH2V_DW
go

declare @from datetime, @to datetime

select
@from = convert(datetime,DATEADD(dd, 1, MAX(Isnull(full_date_alternate_key, '2005-01-01 00:00:00')))), 
@to = DATEADD(dd, 1, CONVERT(varchar(10), getdate(), 101))
from dim_datetime

if @from is null
	set @from = '2011-09-01 00:00:00'
--select @from, @to

declare @date_temp datetime
set @date_temp = @from

while @date_temp < @to
begin
--morning
insert into dim_datetime values
(
Convert(int, CONVERT(nvarchar, @date_temp, 112) + '1'),
@date_temp,
'Morning',
CASE DATEPART(DW, @date_temp)
 WHEN 1 THEN 'Weekend'
 WHEN 2 THEN 'Weekday'
 WHEN 3 THEN 'Weekday'
 WHEN 4 THEN 'Weekday'
 WHEN 5 THEN 'Weekday'
 WHEN 6 THEN 'Weekday'
 WHEN 7 THEN 'Weekend'
 END,
 DATEPART(Month, @date_temp),
 Case DATEPART(Month, @date_temp)
 when 1 then 'Spring'
 when 2 then 'Spring'
 when 3 then 'Spring'
 when 4 then 'Summer'
 when 5 then 'Summer'
 when 6 then 'Summer'
 when 7 then 'Autumn'
 when 8 then 'Autumn'
 when 9 then 'Autumn'
 when 10 then 'Winter'
 when 11 then 'Winter'
 when 12 then 'Winter'
 end
)

--afternoon

insert into dim_datetime values
(
Convert(int, CONVERT(nvarchar, @date_temp, 112) + '2'),
@date_temp,
'Afternoon',
CASE DATEPART(DW, @date_temp)
 WHEN 1 THEN 'Weekend'
 WHEN 2 THEN 'Weekday'
 WHEN 3 THEN 'Weekday'
 WHEN 4 THEN 'Weekday'
 WHEN 5 THEN 'Weekday'
 WHEN 6 THEN 'Weekday'
 WHEN 7 THEN 'Weekend'
 END,
 DATEPART(Month, @date_temp),
 Case DATEPART(Month, @date_temp)
 when 1 then 'Spring'
 when 2 then 'Spring'
 when 3 then 'Spring'
 when 4 then 'Summer'
 when 5 then 'Summer'
 when 6 then 'Summer'
 when 7 then 'Autumn'
 when 8 then 'Autumn'
 when 9 then 'Autumn'
 when 10 then 'Winter'
 when 11 then 'Winter'
 when 12 then 'Winter'
 end
)
--Night
insert into dim_datetime values
(
Convert(int, CONVERT(nvarchar, @date_temp, 112) + '3'),
@date_temp,
'Night',
CASE DATEPART(DW, @date_temp)
 WHEN 1 THEN 'Weekend'
 WHEN 2 THEN 'Weekday'
 WHEN 3 THEN 'Weekday'
 WHEN 4 THEN 'Weekday'
 WHEN 5 THEN 'Weekday'
 WHEN 6 THEN 'Weekday'
 WHEN 7 THEN 'Weekend'
 END,
 DATEPART(Month, @date_temp),
 Case DATEPART(Month, @date_temp)
 when 1 then 'Spring'
 when 2 then 'Spring'
 when 3 then 'Spring'
 when 4 then 'Summer'
 when 5 then 'Summer'
 when 6 then 'Summer'
 when 7 then 'Autumn'
 when 8 then 'Autumn'
 when 9 then 'Autumn'
 when 10 then 'Winter'
 when 11 then 'Winter'
 when 12 then 'Winter'
 end
)

set @date_temp = DATEADD(dd,1,@date_temp)
end


--select * from dim_datetime