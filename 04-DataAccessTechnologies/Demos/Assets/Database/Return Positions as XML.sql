-- =============================================================================
-- Return all positions using as a regular result set
-- =============================================================================

SELECT
   Position.PositionID
  ,Position.CruiseID
  ,Position.ReportedAt
  ,Place.AdminName AS Region
  ,Place.CountryName AS Country
  ,CAST(Position.Latitude AS Decimal(11,8)) AS Latitude
  ,CAST(Position.Longitude AS Decimal(11,8)) AS Longitude
FROM dbo.Positions as Position
JOIN dbo.Places AS Place
ON Position.PlaceID = Place.PlaceID
ORDER BY Position.PositionID

-- =============================================================================
-- Return all positions using FOR XML PATH to create mixed elements
-- =============================================================================

SELECT
   Position.PositionID AS [@PositionID] --The [@...] tells the FOR XML PATH operator to include the value as an attribute
  ,Position.CruiseID AS [@CruiseID]
  ,Position.ReportedAt
  ,Place.AdminName AS Region
  ,Place.CountryName AS Country
  ,CAST(Position.Latitude AS Decimal(11,8)) AS Latitude
  ,CAST(Position.Longitude AS Decimal(11,8)) AS Longitude
FROM dbo.Positions as Position
JOIN dbo.Places AS Place
ON Position.PlaceID = Place.PlaceID
ORDER BY Position.PositionID
FOR XML PATH('Position'), ROOT('Positions')


-- =============================================================================
-- Return a sampling of positions using FOR XML PATH to create mixed elements
-- =============================================================================

SELECT TOP 100
   Position.PositionID AS [@PositionID]
  ,Position.CruiseID AS [@CruiseID]
  ,Position.ReportedAt
  ,Place.AdminName AS Region
  ,Place.CountryName AS Country
  ,CAST(Position.Latitude AS Decimal(11,8)) AS Latitude
  ,CAST(Position.Longitude AS Decimal(11,8)) AS Longitude
FROM dbo.Positions as Position
JOIN dbo.Places AS Place
ON Position.PlaceID = Place.PlaceID
WHERE Position.CruiseID IN (1,2) 
  AND Position.PositionID % 10 = 0
ORDER BY Position.PositionID
FOR XML PATH('Position'), ROOT('Positions')

