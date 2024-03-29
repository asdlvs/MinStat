/****** Script for SelectTopNRows command from SSMS  ******/

With baseLevel as (
SELECT 
	a.Id,
	a.Title,
	a.Part_1,
	a.Part_2,
	a.Part_3,
	a.Part_4,
	a.Part_5,
	(SELECT dbo.CountPersonByEnteprise(1, 1, a.Id, 1, NULL, GETDATE())) as '1.1',
	(SELECT dbo.CountPersonByEnteprise(1, 2, a.Id, 1, NULL, GETDATE())) as '1.2',
	(SELECT dbo.CountPersonByEnteprise(1, 3, a.Id, 1, NULL, GETDATE())) as '1.3',
	(SELECT dbo.CountPersonByEnteprise(2, 1, a.Id, 1, NULL, GETDATE())) as '2.1',
	(SELECT dbo.CountPersonByEnteprise(2, 2, a.Id, 1, NULL, GETDATE())) as '2.2',
	(SELECT dbo.CountPersonByEnteprise(2, 3, a.Id, 1, NULL, GETDATE())) as '2.3',
	(SELECT dbo.CountPersonByEnteprise(3, 1, a.Id, 1, NULL, GETDATE())) as '3.1',
	(SELECT dbo.CountPersonByEnteprise(3, 2, a.Id, 1, NULL, GETDATE())) as '3.2',
	(SELECT dbo.CountPersonByEnteprise(3, 3, a.Id, 1, NULL, GETDATE())) as '3.3',
	(SELECT dbo.CountPersonByEnteprise(4, 1, a.Id, 1, NULL, GETDATE())) as '4.1',
	(SELECT dbo.CountPersonByEnteprise(4, 2, a.Id, 1, NULL, GETDATE())) as '4.2',
	(SELECT dbo.CountPersonByEnteprise(4, 3, a.Id, 1, NULL, GETDATE())) as '4.3'
FROM
dbo.Activities a 
WHERE a.Part_5 <> 0 and a.Part_4 <> 0
)
SELECT * FROM baseLevel
UNION 
SELECT 
	a.Id,
	a.Title,
	a.Part_1,
	a.Part_2,
	a.Part_3,
	a.Part_4,
	a.Part_5,
(SELECT sum(c.[1.1]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[1.2]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[1.3]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[2.1]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[2.2]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[2.3]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[3.1]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[3.2]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[3.3]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[4.1]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[4.2]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4),
	(SELECT sum(c.[4.3]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3 and c.Part_4 = a.Part_4)
 FROM dbo.Activities a WHERE a.Part_5 = 0 and a.Part_4 <> 0
 
 UNION 
SELECT 
	a.Id,
	a.Title,
	a.Part_1,
	a.Part_2,
	a.Part_3,
	a.Part_4,
	a.Part_5,
	(SELECT sum(c.[1.1]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[1.2]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[1.3]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[2.1]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[2.2]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[2.3]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[3.1]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[3.2]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[3.3]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[4.1]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[4.2]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3),
	(SELECT sum(c.[4.3]) FROM baseLevel c Where c.Part_1 = a.Part_1 and c.Part_2 = a.Part_2 and c.Part_3 = a.Part_3)
 FROM dbo.Activities a WHERE a.Part_5 = 0 and a.Part_4 = 0
 
   ORDER BY Id