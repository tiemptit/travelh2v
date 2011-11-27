alter proc pr_getSegment
@budget int,
@companion int,
@familiarity int,
@mood int,
@temperature int,
@travel_length int,
@weather int
as
begin

declare @count int = 0

select @count = COUNT(id)
from real_ratings
where	(@budget = 0 or id_budget = @budget)
	and (@companion = 0 or id_companion = @companion)
	and (@familiarity = 0 or id_farmiliarity = @familiarity)
	and (@mood = 0 or id_mood = @mood)
	and (@temperature = 0 or id_temperature = @temperature)
	and (@travel_length = 0 or id_travel_length = @travel_length)
	and (@weather = 0 or id_weather = @weather)

if (@count > 10)

	select * --id, id_user, id_place, rating
	from real_ratings
	where	(@budget = 0 or id_budget = @budget)
		and (@companion = 0 or id_companion = @companion)
		and (@familiarity = 0 or id_farmiliarity = @familiarity)
		and (@mood = 0 or id_mood = @mood)
		and (@temperature = 0 or id_temperature = @temperature)
		and (@travel_length = 0 or id_travel_length = @travel_length)
		and (@weather = 0 or id_weather = @weather)
else
	select null, null, null

end