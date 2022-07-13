delete from dbo.Accreditation
delete from dbo.Answer
delete from dbo.Question
delete from dbo.Article
delete from dbo.Module
delete from dbo.Survey

DBCC CHECKIDENT ("dbo.Survey", RESEED, 1)
DBCC CHECKIDENT ("dbo.Accreditation", RESEED, 1)
DBCC CHECKIDENT ("dbo.Answer", RESEED, 1)
DBCC CHECKIDENT ("dbo.Question", RESEED, 1)
DBCC CHECKIDENT ("dbo.Article", RESEED, 1)
DBCC CHECKIDENT ("dbo.Module", RESEED, 1)



select *
from Survey

select *
from Module

