CREATE PROCEDURE sp_GetAdviceByTrip 
	@Id int
AS 
BEGIN
	SET NOCOUNT ON
	SELECT AdviceId, Advice.CategoryId, AdviceText, NotificationTime, ImageIcon, TripLeg.TripId, TripLeg.TripLegId,
		TripLeg.Segment, TripLeg.DepartureDate, TripLeg.ArrivalDate, d.AirportName AS DAirportName, a.AirportName AS AAirportName from 
		Advice inner join AdviceCategory on Advice.CategoryId = AdviceCategory.CategoryId
		inner join TripLeg on Advice.TripLegId = TripLeg.TripLegId 
		left join Airport d on TripLeg.DepartureAirportCode = d.AirportCode
		left join Airport a on TripLeg.ArrivalAirportCode = a.AirportCode
		where TripLeg.TripId = @Id Order by TripId, Segment, NotificationTime
END

