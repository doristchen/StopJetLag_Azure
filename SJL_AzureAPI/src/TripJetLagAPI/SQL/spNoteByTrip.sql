CREATE PROCEDURE [dbo].[sp_GetNoteByTrip]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT NoteId, Note, TripLeg.TripId, TripLeg.TripLegId,
	TripLeg.Segment, TripLeg.DepartureDate, TripLeg.ArrivalDate, d.AirportName AS DAirportName, a.AirportName AS AAirportName from 
	LegNote inner join TripLeg on LegNote.TripLegId = TripLeg.TripLegId 
	left join Airport d on TripLeg.DepartureAirportCode = d.AirportCode
	left join Airport a on TripLeg.ArrivalAirportCode = a.AirportCode
	where TripLeg.TripId = @Id Order by TripId, Segment, DepartureDate
END

